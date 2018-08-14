using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuanLyBanSach.Areas.Admin.Models.ChuDeViewModels;
using QuanLyBanSach.Data;
using QuanLyBanSach.Models;

namespace QuanLyBanSach.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class QuanLyChuDeController : Controller
    {
        #region required fields
        ApplicationDbContext context;
        ILogger<QuanLyChuDeController> _logger;
        #endregion
        #region contructor
        public QuanLyChuDeController(ApplicationDbContext _context, ILogger<QuanLyChuDeController> logger)
        {
            _logger = logger;
            context = _context;
        }
        #endregion
        #region Index

        public async Task<IActionResult> Index()
        {
            var model = await context.ChuDe.Include(x=>x.Saches).ToListAsync();
            return View(model);
        }
        #endregion
        #region Thêm chủ đề
        [HttpPost]
        public async Task<IActionResult> ThemChuDe(ThemChuDeViewModel model)
        {
            var chude = new ChuDe
            {
                TenChuDe = model.TenChuDe,
            };
            await context.ChuDe.AddAsync(chude);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Sửa chủ đề
        [HttpPost]
        public async Task<IActionResult> SuaChuDe(int id, ThemChuDeViewModel model)
        {
            var chude = await context.ChuDe.FindAsync(id);
            
            chude.TenChuDe = model.TenChuDe;
            context.ChuDe.Update(chude);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Xóa chủ đề
        public async Task<IActionResult> XoaChuDe(int id)
        {
            var chude = await context.ChuDe.FindAsync(id);
            context.ChuDe.Remove(chude);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                context.Dispose();
            base.Dispose(true);
        }
        #endregion
    }
}