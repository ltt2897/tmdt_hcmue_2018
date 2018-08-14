
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanSach.Data;
using QuanLyBanSach.Models;
using QuanLyBanSach.Models.ContactViewModels;
using X.PagedList;

namespace QuanLyBanSach.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext context { get; set; }
        public HomeController(ApplicationDbContext _context) =>
            context = _context;
        ///<return>
        ///Returns 12 items in database Sach
        ///</return>
        public async Task<IActionResult> Index(int? page)
        {
            ViewData["HeadTitle"] = "Trang chủ";
            ViewData["Title"] = "Sách hay nên đọc";
            var ListSach = await context.Sach.ToListAsync();
            var model = ListSach.ToPagedList(page ?? 1, 9);
            // return View(ListSach);
            return View(model);
        }
        public IActionResult Contact() => View();

        ///<summary>
        //thêm liên hệ vào database
        ///</summary>
        ///<param name="contact">Thông tin liên hê của người dùng gửi lên server</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactViewModel contact)
        {
            if (ModelState.IsValid)
            {
                await context.LienLac.AddAsync(new LienLac
                {
                    Ten = contact.Ten,
                    Email = contact.Email,
                    TieuDe = contact.TieuDe,
                    NoiDung = contact.NoiDung,
                });
                await context.SaveChangesAsync();
            }
            return View(contact);
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }
        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                context.Dispose();
            base.Dispose(true);
        }
    }
}


