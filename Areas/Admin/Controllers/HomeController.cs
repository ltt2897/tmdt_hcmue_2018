using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuanLyBanSach.Models;
using QuanLyBanSach.Areas.Admin.Models;
using QuanLyBanSach.Data;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace QuanLyBanSach.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {

        ApplicationDbContext context;
        UserManager<ApplicationUser> userManager;
        RoleManager<IdentityRole> roleManager;
        public HomeController(RoleManager<IdentityRole> _roleManager,
                                ApplicationDbContext _context, 
                                UserManager<ApplicationUser> _usermanager)
        {
            context = _context;
            userManager = _usermanager;
            roleManager = _roleManager;
        }
        [Route("[area]")]
        public async Task<IActionResult> DashBoard()
        {
            var _dashBoard = new DashBoardViewModel
            {
                TongSoLuongSach = context.Sach.Count(),
                TongDoanhThuTrongThang = context.HoaDon
                                                .Where(x => x.NgayLapHoaDon.Year == DateTime.Now.Year && x.NgayLapHoaDon.Month == DateTime.Now.Month)
                                                .Sum(x => x.TongThanhTien),
                TongSoKhachHang = (await userManager.GetUsersInRoleAsync("User")).Count(),
            };
            return View(_dashBoard);
        }

        public IActionResult ThongKeDoanhThu()
        {
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
                userManager.Dispose();
            }
        }
    }
}