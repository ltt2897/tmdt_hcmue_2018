using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhuKienDienThoai.Areas.Admin.Models.MatHangViewModels;
using PhuKienDienThoai.Data;
using PhuKienDienThoai.Models;

namespace PhuKienDienThoai.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class QuanLyMatHangController : Controller
    {
        #region required fields
        ApplicationDbContext context;
        ILogger<QuanLyMatHangController> _logger;
        #endregion
        #region contructor
        public QuanLyMatHangController(ApplicationDbContext _context, ILogger<QuanLyMatHangController> logger)
        {
            _logger = logger;
            context = _context;
        }
        #endregion
        #region Index

        public async Task<IActionResult> Index()
        {
            ViewData["TagName"] = "QuanLyMatHang";
            var model = await context.MatHang.Include(x=>x.SanPhames).ToListAsync();
            return View(model);
        }
        #endregion
        #region Thêm mặt hàng
        [HttpPost]
        public async Task<IActionResult> ThemMatHang(ThemMatHangViewModel model)
        {
            var mathang = new MatHang
            {
                TenMatHang = model.TenMatHang,
            };
            await context.MatHang.AddAsync(mathang);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Sửa mặt hàng
        [HttpPost]
        public async Task<IActionResult> SuaMatHang(int id, ThemMatHangViewModel model)
        {
            var mathang = await context.MatHang.FindAsync(id);
            
            mathang.TenMatHang = model.TenMatHang;
            context.MatHang.Update(mathang);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Xóa mặt hàng
        public async Task<IActionResult> XoaMatHang(int id)
        {
            var mathang = await context.MatHang.FindAsync(id);
            context.MatHang.Remove(mathang);
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