using System;
using System.ComponentModel.DataAnnotations;

namespace PhuKienDienThoai.Areas.Admin.Models.NguoiDungViewModels
{
    public class SuaNguoiDungViewModel
    {
        [Required(ErrorMessage = "Họ tên là bắt buộc")]
        [Display(Name = "Họ tên")]
        public string HoTen { get; set; }
        [Required]
        [Display(Name = "ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime NgaySinh { get; set; }
        public bool GioiTinh { get; set; }
        [Required(ErrorMessage = "Địa chỉ là bắt buộc")]
        [Display(Name = "Địa chỉ")]
        public string DiaChi { get; set; }

        [Display(Name = "Chức vụ")]
        public string ChucVu { get; set; }

        [Required(ErrorMessage = "Email là bắt buộc")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "số điện thoại là bắt buộc")]
        [Display(Name = "số điện thoại")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Mật khẩu Mới")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}