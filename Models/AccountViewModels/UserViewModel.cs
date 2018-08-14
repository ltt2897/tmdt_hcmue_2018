using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLyBanSach.Models.AccountViewModels
{
    public class UserViewModel
    {
        [Display(Name="Họ Tên")]
        public string HoTen { get; set; }
        [Display(Name="Số điện thoại")]
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        [Display(Name="Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime NgaySinh { get; set; }
        [Display(Name="Ngày sinh")]
        public bool GioiTinh { get; set; }
        [Display(Name="Địa chỉ giao hàng Mặc định")]
        public string DiaChi { get; set; }
    }
}