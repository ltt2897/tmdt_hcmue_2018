using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanSach.Areas.Admin.Models.TacGiaViewModels;
using QuanLyBanSach.Data;
using QuanLyBanSach.Models;

namespace QuanLyBanSach.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class QuanLyTacGiaController : Controller
    {
        #region các field cần thiết
        ApplicationDbContext context;
        #endregion
        #region contructor
        public QuanLyTacGiaController(ApplicationDbContext _context) => context = _context;
        #endregion
        #region Index

        public async Task< IActionResult> Index()
        {
            var model = await context.TacGia.Include(s=>s.Saches).ToListAsync();
            return View(model);
        }
        #endregion
        #region XoaTacGia
        public async Task<IActionResult> XoaTacGia(int id)
        {
            var tacgia = await context.TacGia.FindAsync(id);
            context.TacGia.Remove(tacgia);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region ThemTacGia
        [HttpPost]
        public async Task<IActionResult> ThemTacGia(ThemTacGiaViewModel model)
        {
            var tacgia = new TacGia
            {
                TenTacGia = model.TenTacGia,
            };
            await context.AddAsync(tacgia);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region SuaTacGia
        [HttpPost]
        public async Task<IActionResult> SuaTacGia(int id, ThemTacGiaViewModel model)
        {
            var tacgia = await context.TacGia.FindAsync(id);
            tacgia.TenTacGia = model.TenTacGia;
            context.Update(tacgia);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                context.Dispose();
        }
        #endregion
    }
}