using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanSach.Areas.GiaoHang.Models.GiaoHangViewModels;
using QuanLyBanSach.Data;
using QuanLyBanSach.Models;

namespace QuanLyBanSach.Areas.GiaoHang.Controllers
{
    [Area("GiaoHang")]
    [Authorize(Roles = "Giao Hàng")]
    public class HomeController : Controller
    {
        #region required fields
        ApplicationDbContext context;
        UserManager<ApplicationUser> userManager;
        #endregion
        public HomeController(ApplicationDbContext _context, UserManager<ApplicationUser> _usermanager)
        {
            context = _context;
            userManager = _usermanager;
        }

        [Route("[Area]/[Controller]")]
        public async Task<IActionResult> ChuaGiao()
        {
            var data = await context.HoaDon.Include(x=>x.User).Where(x => x.NgayGiao == null)
                                            .ToListAsync();
            var model = new ChuaGiaoViewModel();
            foreach (var item in data)
            {
                model.DanhSachHoaDon.Add(new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.User.HoTen,
                });
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GiaoHang(int Id)
        {
            var hoadon = await context.HoaDon.FindAsync(Id);
            hoadon.NgayGiao = DateTime.Now;
            context.HoaDon.Update(hoadon);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(ChuaGiao));
        }
        public async Task<IActionResult> DaGiao()
        {
            var data = await context.HoaDon.Where(x => x.NgayGiao != null)
                                           .ToListAsync();
            return View(data);
        }
        [Route("[Area]/[Controller]/[Action]/{id:int}")]
        [AllowAnonymous]
        public async Task<JsonResult> ChiTietDonHang(int Id)
        {
            var donhang = await context.ChiTietHoaDon.Include(x => x.HoaDon)
                                                    .Include(x => x.Sach)
                                                    .Where(x => x.HoaDonId == Id)
                                                    .Select(x => new
                                                    {
                                                        x.Sach.TenSach,
                                                        x.Sach.DonGia,
                                                        x.SoLuong,
                                                        x.ThanhTien
                                                    })
                                                    .ToListAsync();
            return Json(new
            {
                data = donhang
            });
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
                userManager.Dispose();
            }
            base.Dispose(true);
        }
    }
}