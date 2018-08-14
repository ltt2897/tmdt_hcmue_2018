using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuanLyBanSach.Areas.Admin.Models.DanhMucViewmodels;
using QuanLyBanSach.Data;
using QuanLyBanSach.Models;

namespace QuanLyBanSach.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class QuanLyDanhMucController : Controller
    {
        #region required fields
        ApplicationDbContext context;
        #endregion
        #region contructor
        public QuanLyDanhMucController(ApplicationDbContext _context) => context = _context;
        #endregion
        #region Index
        public async Task<IActionResult> Index()
        {
            var model = await context.DanhMuc.Include(p => p.Saches).ToListAsync();
            return View(model);
        }
        #endregion
        #region Thêm danh mục
        [HttpPost]
        public async Task<IActionResult> ThemDanhMuc(ThemDanhMucViewModel model)
        {
            var danhmuc = new DanhMuc
            {
                TenDanhMuc = model.TenDanhMuc,
            };
            await context.DanhMuc.AddAsync(danhmuc);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Xóa danh mục
        public async Task<IActionResult> XoaDanhMuc(int id)
        {
            var danhmuc = await context.DanhMuc.FindAsync(id);
            context.DanhMuc.Remove(danhmuc);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Sửa danh mục
        [HttpPost]
        public async Task<IActionResult> SuaDanhMuc(int id, ThemDanhMucViewModel model)
        {
            var danhmuc = await context.DanhMuc.FindAsync(id);
            danhmuc.TenDanhMuc = model.TenDanhMuc;
            context.DanhMuc.Update(danhmuc);
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