using System.ComponentModel.DataAnnotations;

namespace QuanLyBanSach.Areas.Admin.Models.NhaXuatBanViewModels
{
    public class ThemNhaXuatBanViewModel
    {
        [Required]
        public string TenNhaXuatBan { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required]
        public string SoDienThoai { get; set; }
    }
}