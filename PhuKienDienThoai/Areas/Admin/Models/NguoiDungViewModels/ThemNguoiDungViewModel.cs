using System;
using System.ComponentModel.DataAnnotations;

namespace PhuKienDienThoai.Areas.Admin.Models.NguoiDungViewModels
{
    public class ThemNguoiDungViewModel
    {
        [Required(ErrorMessage = "Họ tên là bắt buộc")]
        [Display(Name = "Họ tên")]
        public string HoTen { get; set; }

        [Required]
        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime NgaySinh { get; set; }

        [Display(Name = "Giới Tính")]
        public bool GioiTinh { get; set; } = true;

        [Display(Name = "Địa chỉ")]
        [Required(ErrorMessage = "Địa chỉ là bắt buộc")]
        public string DiaChi { get; set; }

        [Display(Name = "Chức vụ")]
        public string ChucVu { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "số điện thoại là bắt buộc")]
        [Display(Name = "số điện thoại")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
        [Display(Name = "Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
    }
}