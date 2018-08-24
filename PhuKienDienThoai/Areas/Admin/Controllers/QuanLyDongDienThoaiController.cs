using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuKienDienThoai.Areas.Admin.Models.DongDienThoaiViewModels;
using PhuKienDienThoai.Data;
using PhuKienDienThoai.Models;

namespace PhuKienDienThoai.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class QuanLyDongDienThoaiController : Controller
    {
        #region các field cần thiết
        ApplicationDbContext context;
        #endregion
        #region contructor
        public QuanLyDongDienThoaiController(ApplicationDbContext _context) => context = _context;
        #endregion
        #region Index

        public async Task< IActionResult> Index()
        {
            ViewData["TagName"] = "QuanLyDongDienThoai";
            var model = await context.DongDienThoai.Include(s=>s.SanPhames).ToListAsync();
            return View(model);
        }
        #endregion
        #region XoaDongDienThoai
        public async Task<IActionResult> XoaDongDienThoai(int id)
        {
            var DongDienThoai = await context.DongDienThoai.FindAsync(id);
            context.DongDienThoai.Remove(DongDienThoai);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region ThemDongDienThoai
        [HttpPost]
        public async Task<IActionResult> ThemDongDienThoai(ThemDongDienThoaiViewModel model)
        {
            var DongDienThoai = new DongDienThoai
            {
                TenDongDienThoai = model.TenDongDienThoai,
            };
            await context.AddAsync(DongDienThoai);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region SuaDongDienThoai
        [HttpPost]
        public async Task<IActionResult> SuaDongDienThoai(int id, ThemDongDienThoaiViewModel model)
        {
            var DongDienThoai = await context.DongDienThoai.FindAsync(id);
            DongDienThoai.TenDongDienThoai = model.TenDongDienThoai;
            context.Update(DongDienThoai);
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