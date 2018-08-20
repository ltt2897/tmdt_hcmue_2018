using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuKienDienThoai.Data;
using X.PagedList;

namespace PhuKienDienThoai.Controllers
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
            var data = await context.SanPham
                                    .Include(x => x.DanhMuc)
                                    .Where(x => x.DanhMuc.Id == id)
                                    .ToListAsync();
            var DanhMuc = await context.DanhMuc.FindAsync(id);
            var MatHang = await context.MatHang.FindAsync(DanhMuc.MatHangId);
            ViewData["SubTitle"] = MatHang.TenMatHang;
            ViewData["HeadTitle"] = DanhMuc.TenDanhMuc;
            ViewData["Title"] = ViewData["SubTitle"] + " cho sản phẩm " + ViewData["HeadTitle"];

            var model = data.ToPagedList(page ?? 1, 9);
            return View("Views/Home/Index.cshtml", model);
            // return View("Views/Home/Index.cshtml",data);
        }
        [Route("[Controller]/[Action]")]
        public async Task<IActionResult> SanPhamGiamGia(int? page)
        {
            var data = await context.SanPham.Where(x => x.PhanTramGiamGia != 0)
                                        .ToListAsync();
            ViewData["HeadTitle"] = "Sản phẩm giảm giá";
            ViewData["Title"] = "Sản phẩm giảm giá";

            var model = data.ToPagedList(page ?? 1, 9);
            return View("Views/Home/Index.cshtml", model);
            // return View("Views/Home/Index.cshtml",data);
        }

    }
}