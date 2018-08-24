using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PhuKienDienThoai.Areas.Admin.Models.ThuongHieuViewModels;
using PhuKienDienThoai.Data;
using PhuKienDienThoai.Models;

namespace PhuKienDienThoai.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class QuanLyThuongHieuController : Controller
    {
        #region required fields
        ILogger<QuanLyThuongHieuController> _logger;
        ApplicationDbContext context;
        #endregion
        #region contrutor
        public QuanLyThuongHieuController(ApplicationDbContext _context, ILogger<QuanLyThuongHieuController> logger)
        {
            _logger = logger;
            context = _context;
        }
        #endregion
        #region index
        public async Task<IActionResult> Index()
        {
            ViewData["TagName"] = "QuanLyThuongHieu";
            var model = await context.ThuongHieu.Include(s => s.SanPhames).ToListAsync();
            return View(model);
        }
        #endregion
        #region Thêm thương hiệu
        [HttpPost]
        public async Task<IActionResult> ThemThuongHieu(ThemThuongHieuViewModel model)
        {
            var nxb = new ThuongHieu
            {
                TenThuongHieu = model.TenThuongHieu,
            };
            await context.ThuongHieu.AddAsync(nxb);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Xóa thương hiệu
        public async Task<IActionResult> XoaThuongHieu(int? id)
        {
            if (id == null)
                return BadRequest();

            var nxb = await context.ThuongHieu.FindAsync(id.Value);
            if (nxb == null)
                return BadRequest();

            context.ThuongHieu.Remove(nxb);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Sửa thương hiệu
        [HttpPost]
        public async Task<IActionResult> SuaThuongHieu(int id, ThemThuongHieuViewModel model)
        {
            var nxb = await context.ThuongHieu.FindAsync(id);
            nxb.TenThuongHieu = model.TenThuongHieu;
            context.ThuongHieu.Update(nxb);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

    }
}