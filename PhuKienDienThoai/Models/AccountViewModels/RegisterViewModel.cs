using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhuKienDienThoai.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Mật khẩu {0} phải từ {2} kí tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name="Số điện thoại")]
        [Required(ErrorMessage="Vui lòng nhập số điện thoại")]
        public string  PhoneNumber { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Xác nhận mật khẩu chưa trùng khớp với mật khẩu.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Họ tên là bắt buộc")]
        [Display(Name = "Họ tên")]
        public string HoTen { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage="Ngày sinh là bắt buộc")]
        [Display(Name="Ngày sinh")]
        public DateTime NgaySinh { get; set; }

        [Display(Name="Giới tính")]
        public bool GioiTinh { get; set; }
        
        [Display(Name="Địa chỉ")]
        [Required(ErrorMessage="Địa chỉ là bắt buộc")]
        public string DiaChi { get; set; }

    }
}
