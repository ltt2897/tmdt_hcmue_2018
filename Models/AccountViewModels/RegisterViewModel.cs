using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuanLyBanSach.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Display(Name="Số điện thoại")]
        [Required(ErrorMessage="Vui lòng nhập số điện thoại")]
        public string  PhoneNumber { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Họ tên là bắt buộc")]
        [Display(Name = "Họ tên")]
        public string HoTen { get; set; }
        [DataType(DataType.Date)]
        [Required(ErrorMessage="Ngày sinh là bắt buộc")]
        public DateTime NgaySinh { get; set; }
        [Display(Name="Giới tính")]
        public bool GioiTinh { get; set; }
        [Display(Name="Địa chỉ")]
        [Required(ErrorMessage="Địa chỉ là bắt buộc")]
        public string DiaChi { get; set; }

    }
}
