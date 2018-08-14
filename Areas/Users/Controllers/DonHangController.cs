using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyBanSach.Data;
using QuanLyBanSach.Models;
using X.PagedList;

namespace QuanLyBanSach.Areas.Users.Controllers
{
    [Area("Users")]
    [Authorize(Roles = "User")]
    public class DonHangController : Controller
    {
        private ApplicationDbContext context;
        private UserManager<ApplicationUser> userManager;
        public DonHangController(ApplicationDbContext _context, UserManager<ApplicationUser> _usermanager)
        {
            userManager = _usermanager;
            context = _context;
        }

        public async Task<IActionResult> Index(int? page)
        {

            var user = await userManager.GetUserAsync(User);
            var userId = await userManager.GetUserIdAsync(user);
            var danhsachDonHang = context.HoaDon.Include(x => x.ChiTietHoaDons)
                                                    .ThenInclude(x => x.Sach)
                                                .Where(x => x.User.Id == userId)
                                                .ToList();
            return View(danhsachDonHang.ToPagedList(page ?? 1, 5));
        }
        public async Task<IActionResult> ChiTietDonHang(int Id)
        {
            var data = await context.ChiTietHoaDon.Include(x => x.HoaDon)
                                                    .Include(x => x.Sach)
                                                    .Where(x => x.HoaDonId == Id).ToListAsync();
            return View(data);
        }

        override protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
                userManager.Dispose();
            }
        }
    }
}