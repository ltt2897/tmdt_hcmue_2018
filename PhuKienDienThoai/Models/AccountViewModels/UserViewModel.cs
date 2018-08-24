using System;
using System.ComponentModel.DataAnnotations;

namespace PhuKienDienThoai.Models.AccountViewModels
{
    public class UserViewModel
    {
        [Display(Name="Họ tên")]
        public string HoTen { get; set; }

        [Display(Name="Số điện thoại")]
        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        [DataType(DataType.Date)]
        [Display(Name="Ngày sinh")]
        public DateTime NgaySinh { get; set; }

        public bool GioiTinh { get; set; }

        [Display(Name="Địa chỉ giao hàng mặc định")]
        public string DiaChi { get; set; }
        
    }
}