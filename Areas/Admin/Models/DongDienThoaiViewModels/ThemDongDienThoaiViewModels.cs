using System.ComponentModel.DataAnnotations;

namespace PhuKienDienThoai.Areas.Admin.Models.DongDienThoaiViewModels
{
    public class ThemDongDienThoaiViewModel
    {
        [Required]
        public string TenDongDienThoai { get; set; }
    }
}