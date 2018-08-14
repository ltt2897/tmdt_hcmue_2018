using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuanLyBanSach.Areas.Admin.Models.NhaXuatBanViewModels;
using QuanLyBanSach.Data;
using QuanLyBanSach.Models;

namespace QuanLyBanSach.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class QuanLyNhaXuatBanController : Controller
    {
        #region required fields
        ILogger<QuanLyNhaXuatBanController> _logger;
        ApplicationDbContext context;
        #endregion
        #region contrutor
        public QuanLyNhaXuatBanController(ApplicationDbContext _context, ILogger<QuanLyNhaXuatBanController> logger)
        {
            _logger = logger;
            context = _context;
        }
        #endregion
        #region index
        public async Task<IActionResult> Index()
        {
            var model = await context.NhaXuatBan.Include(s => s.Saches).ToListAsync();
            return View(model);
        }
        #endregion
        #region Thêm nhà xuất bản
        [HttpPost]
        public async Task<IActionResult> ThemNhaXuatBan(ThemNhaXuatBanViewModel model)
        {
            var nxb = new NhaXuatBan
            {
                TenNhaXuatBan = model.TenNhaXuatBan,
                SoDienThoai = model.SoDienThoai,
            };
            await context.NhaXuatBan.AddAsync(nxb);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Xóa nhà xuất bản
        public async Task<IActionResult> XoaNhaXuatBan(int? id)
        {
            if (id == null)
                return BadRequest();

            var nxb = await context.NhaXuatBan.FindAsync(id.Value);
            if (nxb == null)
                return BadRequest();

            context.NhaXuatBan.Remove(nxb);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Sửa nhà xuất bản
        [HttpPost]
        public async Task<IActionResult> SuaNhaXuatBan(int id, ThemNhaXuatBanViewModel model)
        {
            var nxb = await context.NhaXuatBan.FindAsync(id);
            nxb.TenNhaXuatBan = model.TenNhaXuatBan;
            nxb.SoDienThoai = model.SoDienThoai;
            context.NhaXuatBan.Update(nxb);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

    }
}