using System.ComponentModel.DataAnnotations;

namespace PhuKienDienThoai.Areas.Admin.Models.ThuongHieuViewModels
{
    public class ThemThuongHieuViewModel
    {
        [Required]
        public string TenThuongHieu { get; set; }
    }
}