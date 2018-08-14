using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanSach.Data;
using X.PagedList;

namespace QuanLyBanSach.Controllers
{
    public class NhaXuatBanController : Controller
    {
        ApplicationDbContext context;
        public NhaXuatBanController(ApplicationDbContext _context) => context = _context;
        [HttpGet("[Controller]/{id}")]
        public async Task<IActionResult> Index(int id, int? page)
        {
            var data = await context.Sach
                                    .Include(x => x.NhaXuatBan)
                                    .Where(x => x.NhaXuatBan.Id == id)
                                    .ToListAsync();

            var nhaXuatBan = await context.NhaXuatBan.FindAsync(id);

            ViewData["HeadTitle"] = nhaXuatBan.TenNhaXuatBan;
            ViewData["Title"] = "Sách theo nhà xuất bản " + ViewData["HeadTitle"];
            var model = data.ToPagedList(page ?? 1, 9);
            return View("Views/Home/Index.cshtml", model);
            // return View("Views/Home/Index.cshtml",data);
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                context.Dispose();
            base.Dispose(true);
        }
    }
}