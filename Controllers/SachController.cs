using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanSach.Data;
using QuanLyBanSach.Models.SachViewModels;
using Microsoft.AspNetCore.Identity;
using QuanLyBanSach.Models;
using Microsoft.AspNetCore.Http;

namespace QuanLyBanSach.Controllers
{
    public class SachController : Controller
    {
        ApplicationDbContext context;
        private UserManager<ApplicationUser> _userManager;
        public SachController(ApplicationDbContext _context, UserManager<ApplicationUser> userManager)
        {
            context = _context;
            _userManager = userManager;
        }
        /*
        route Tensach chủ yếu làm cho đường dẫn trong đẹp hơn và cầu kỳ hơn chứ tên sách cũng không có chức năng gì khác
         */
        [HttpGet("[Controller]/{TenSach}-{id}")]
        public async Task<IActionResult> Index(string TenSach, int id)
        {
            var sach = await context.Sach
                                    .Include(x => x.TacGia)
                                    .Include(x => x.NhaXuatBan)
                                    .Include(x => x.DanhMuc)
                                    .FirstOrDefaultAsync(x => x.id == id);
            if (sach != null)
            {
                ViewData["HeadTitle"] = sach.TenSach;
                ViewData["TinhTrang"] = (sach.SoLuong > 0) ? "Còn hàng" : "hết hàng";
            }
            else
            {
                ViewData["HeadTitle"] = "Error";
            }
            return View(sach);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemNhanXet(NhanXetViewModel nhanxet)
        {
            var sach = await context.Sach.FindAsync(nhanxet.SachId);
            try
            {
                var getUser = await _userManager.GetUserAsync(HttpContext.User);
                if (getUser == null)
                {
                    ViewData["message"] = "Bạn phải đăng nhập trước";
                    return RedirectToAction("index", new { id = nhanxet.SachId, TenSach = sach.TenSach.Replace(' ', '-') });
                }

                await context.NhanXet.AddAsync(new Models.NhanXet
                {
                    TieuDe = nhanxet.TieuDe,
                    NoiDung = nhanxet.NoiDung,
                    Sach = sach,
                    User = getUser
                });
                context.SaveChanges();
                return RedirectToAction(
                    actionName: "Index",
                    routeValues: new
                    {
                        id = nhanxet.SachId,
                        TenSach = sach.TenSach.Replace(' ', '-')
                    }
                );
                // return Ok(new
                // {
                //     error= false,
                //     message = "Thêm thành công",
                // });
            }
            catch (System.Exception ex)
            {
                Logs.Logger.Log(ex.ToString());
                // return Ok(new
                // {
                //     error = true,
                //     message = "có lỗi xảy ra trong quá trình thực hiện",
                // });

                ViewData["message"] = "Bạn phải đăng nhập trước";
                return RedirectToAction("index", new { id = nhanxet.SachId, TenSach = sach.TenSach.Replace(' ', '-') });
            }

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
                _userManager.Dispose();
            }
            base.Dispose(true);
        }

    }
}