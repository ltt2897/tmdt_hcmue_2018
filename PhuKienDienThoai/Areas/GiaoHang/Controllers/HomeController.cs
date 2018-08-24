using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhuKienDienThoai.Areas.GiaoHang.Models.GiaoHangViewModels;
using PhuKienDienThoai.Data;
using PhuKienDienThoai.Models;

namespace PhuKienDienThoai.Areas.GiaoHang.Controllers
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
                    Text = item.Id.ToString() + " / " + item.NgayLapHoaDon.ToString() + " / " + item.User.HoTen,
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
            var chitietdonhang = await context.ChiTietHoaDon.Include(x => x.HoaDon)
                                                    .Include(x => x.SanPham)
                                                    .Where(x => x.HoaDonId == Id)
                                                    .Select(x => new
                                                    {
                                                        x.SanPham.TenSanPham,
                                                        x.SanPham.DonGia,
                                                        x.SoLuong,
                                                        x.ThanhTien
                                                    })
                                                    .ToListAsync();
            return Json(new
            {
                data = chitietdonhang
            });
        }
        [Route("[Area]/[Controller]/[Action]/{id:int}")]
        [AllowAnonymous]
        public JsonResult ChiTietHoaDon(int Id)
        {
            var donhang = context.HoaDon.Where(x => x.Id == Id).FirstOrDefault();
        
            return Json(new
            {
                diachi = donhang.DiaChi,
                ghichu = donhang.GhiChu,
                phuongthucthanhtoan = donhang.PhuongThucThanhToan
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