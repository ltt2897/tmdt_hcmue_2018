using System.ComponentModel.DataAnnotations;

namespace QuanLyBanSach.Areas.Admin.Models.TacGiaViewModels
{
    public class ThemTacGiaViewModel
    {
        [Required]
        public string TenTacGia { get; set; }
    }
}