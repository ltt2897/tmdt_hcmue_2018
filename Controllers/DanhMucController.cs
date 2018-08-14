using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanSach.Data;
using X.PagedList;

namespace QuanLyBanSach.Controllers
{
    public class DanhMucController : Controller
    {
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                context.Dispose();
            base.Dispose(true);
        }
        ApplicationDbContext context;
        public DanhMucController(ApplicationDbContext _context) => context = _context;
        [HttpGet("[Controller]/{id:int}")]
        public async Task<IActionResult> Index(int id, int? page)
        {
            var data = await context.Sach
                                    .Include(x => x.DanhMuc)
                                    .Where(x => x.DanhMuc.Id == id)
                                    .ToListAsync();
            var nhaXuatBan = await context.DanhMuc.FindAsync(id);
            ViewData["HeadTitle"] = nhaXuatBan.TenDanhMuc;
            ViewData["Title"] = "Sách theo Danh mục " + ViewData["HeadTitle"];

            var model = data.ToPagedList(page ?? 1, 9);
            return View("Views/Home/Index.cshtml", model);
            // return View("Views/Home/Index.cshtml",data);
        }
        [Route("[Controller]/[Action]")]
        public async Task<IActionResult> SachGiamGia(int? page)
        {
            var data = await context.Sach.Where(x => x.PhanTramGiamGia != 0)
                                        .ToListAsync();
            ViewData["HeadTitle"] = "Sách giảm giá";
            ViewData["Title"]="Sách giảm giá";

            var model = data.ToPagedList(page ?? 1, 9);
            return View("Views/Home/Index.cshtml", model);
            // return View("Views/Home/Index.cshtml",data);
        }

    }
}