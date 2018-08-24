using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuKienDienThoai.Data;
using PhuKienDienThoai.Models.SanPhamViewModels;
using Microsoft.AspNetCore.Identity;
using PhuKienDienThoai.Models;
using Microsoft.AspNetCore.Http;

namespace PhuKienDienThoai.Controllers
{
    public class SanPhamController : Controller
    {
        ApplicationDbContext context;
        private UserManager<ApplicationUser> _userManager;
        public SanPhamController(ApplicationDbContext _context, UserManager<ApplicationUser> userManager)
        {
            context = _context;
            _userManager = userManager;
        }
        /*
        route Tensanpham chủ yếu làm cho đường dẫn trong đẹp hơn và cầu kỳ hơn chứ tên sản phẩm cũng không có chức năng gì khác
         */
        [HttpGet("[Controller]/{TenSanPham}-{id}.html")]
        public async Task<IActionResult> Index(int id, string TenSanPham)
        {
            var sanpham = await context.SanPham
                                    .Include(x => x.DongDienThoai)
                                    .Include(x => x.ThuongHieu)
                                    .Include(x => x.MatHang)
                                    .Include(x => x.DanhMuc)
                                    .FirstOrDefaultAsync(x => x.id == id);
            if (sanpham != null)
            {
                ViewData["HeadTitle"] = sanpham.TenSanPham + " - PhuKienDienThoai.com";
                ViewData["TinhTrang"] = (sanpham.SoLuong > 0) ? "Còn hàng" : "hết hàng";
            }
            else
            {
                ViewData["HeadTitle"] = "Error";
            }
            return View(sanpham);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemNhanXet(NhanXetViewModel nhanxet)
        {
            var sanpham = await context.SanPham.FindAsync(nhanxet.SanPhamId);
            try
            {
                var getUser = await _userManager.GetUserAsync(HttpContext.User);
                if (getUser == null)
                {
                    ViewData["message"] = "Bạn phải đăng nhập trước";
                    return RedirectToAction("index", new { id = nhanxet.SanPhamId, TenSanPham = sanpham.TenSanPham.Replace(' ', '-') });
                }

                await context.NhanXet.AddAsync(new Models.NhanXet
                {
                    TieuDe = nhanxet.TieuDe,
                    NoiDung = nhanxet.NoiDung,
                    SanPham = sanpham,
                    User = getUser
                });
                context.SaveChanges();
                return RedirectToAction(
                    actionName: "Index",
                    routeValues: new
                    {
                        id = nhanxet.SanPhamId,
                        TenSanPham = sanpham.TenSanPham.Replace(' ', '-')
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
                return RedirectToAction("index", new { id = nhanxet.SanPhamId, TenSanPham = sanpham.TenSanPham.Replace(' ', '-') });
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