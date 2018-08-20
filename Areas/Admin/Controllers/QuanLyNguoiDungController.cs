using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PhuKienDienThoai.Areas.Admin.Models.NguoiDungViewModels;
using PhuKienDienThoai.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using PhuKienDienThoai.Data;

namespace PhuKienDienThoai.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class QuanLyNguoiDungController : Controller
    {
        #region required fields
        UserManager<ApplicationUser> usermanager;
        ApplicationDbContext context;

        #endregion

        #region contructor
        public QuanLyNguoiDungController(ApplicationDbContext _context, UserManager<ApplicationUser> _usermanager)
        {
            usermanager = _usermanager;
            context = _context;
        }
        #endregion

        #region View Danh sách người dùng
        public async Task<IActionResult> Index()
        {
            ViewData["TagName"] = "QuanLyNguoiDung";
            var data = await DanhSachNguoiDung();
            return View(data);
        }
        #endregion

        #region lấy Danh sách người dùng
        private async Task<List<ApplicationUser>> DanhSachNguoiDung()
        {
            var allUsers = await usermanager.Users.ToListAsync();
            return allUsers;
        }
        #endregion

        ///<summary>
        ///lấy danh sách chức vụ (Không có người mua(user))
        ///<summary>
        public async Task<List<IdentityRole>> getChucVuListAsync()
        {
            var ChucVuList = await context.Roles.ToListAsync();
            return ChucVuList;
        }
        #region view Thêm người dùng
        public async Task<IActionResult> ThemNguoiDung()
        {
            var model = new ThemNguoiDungViewModel();
            ViewBag.ChucVu = await getChucVuListAsync();
            return View(model);
        }
        #endregion

        #region action Thêm người dùng
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemNguoiDung(ThemNguoiDungViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //lấy dữ liệu chức vụ list
                ViewBag.ChucVu = await getChucVuListAsync();
                return View(model);
            }
            var newuser = new ApplicationUser
            {
                HoTen = model.HoTen,
                DiaChi = model.DiaChi,
                Email = model.Email,
                EmailConfirmed = true,
                UserName = model.Email,
                GioiTinh = model.GioiTinh,
                PhoneNumber = model.PhoneNumber,
                NgaySinh = model.NgaySinh
            };
            await usermanager.CreateAsync(newuser);
            await usermanager.AddToRoleAsync(newuser, model.ChucVu);
            return RedirectToAction(
                actionName: "DanhSanPham",
                controllerName: "QuanLyNhanVien"
            );
        }
        #endregion

        #region action Xóa người dùng
        public async Task<IActionResult> XoaNguoiDung(string email)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
            {//nếu không có email thì trở lại trang trước đó
                return NotFound();
            }
            var DeleteUser = await usermanager.FindByEmailAsync(email);
            await usermanager.DeleteAsync(DeleteUser);
            return RedirectToAction(nameof(Index));
        }
        #endregion

        #region view sửa người dùng
        public async Task<IActionResult> SuaNguoiDung(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return NotFound();
            }
            var getuser = await context.Users.Where(data => data.Email == email).FirstOrDefaultAsync();
            ViewBag.ChucVu = context.Roles;
            var model = new SuaNguoiDungViewModel
            {
                ChucVu = (await usermanager.GetRolesAsync(getuser)).FirstOrDefault(),
                DiaChi = getuser.DiaChi,
                HoTen = getuser.HoTen,
                GioiTinh = getuser.GioiTinh,
                Email = getuser.Email,
                NgaySinh = getuser.NgaySinh,
                PhoneNumber = getuser.PhoneNumber
            };

            return View(model);
        }
        #endregion

        #region action Sửa người dùng
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuaNguoiDung(SuaNguoiDungViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ChucVu = context.Roles;
                return View(model);
            }
            var currentUser = await usermanager.FindByEmailAsync(model.Email);
            currentUser.HoTen = model.HoTen;
            currentUser.NgaySinh = model.NgaySinh;
            currentUser.DiaChi = model.DiaChi;
            currentUser.PhoneNumber = model.PhoneNumber;
            //Email và giới tính không thay đổi
            await usermanager.UpdateAsync(currentUser);
            //update chức vụ bằng cách xóa đi chức vụ cũ và thêm vào chức vụ mới
            await usermanager.RemoveFromRoleAsync(currentUser, (await usermanager.GetRolesAsync(currentUser)).FirstOrDefault());
            await usermanager.AddToRoleAsync(currentUser, model.ChucVu);
            //nếu mật khẩu không rỗng thì đổi mật khẩu
            if (!string.IsNullOrEmpty(model.Password))
            {

                var token = await usermanager.GeneratePasswordResetTokenAsync(currentUser);
                var chngpsswrd = await usermanager.ResetPasswordAsync(currentUser, token, model.Password);
                if (!chngpsswrd.Succeeded)
                {
                    return View(model);
                }
            }
            //update xong thì trả về danh sách
            return Redirect(nameof(Index));
        }

        #endregion

        #region Dispose
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                usermanager.Dispose();
                context.Dispose();
            }
        }
        #endregion
    }
}