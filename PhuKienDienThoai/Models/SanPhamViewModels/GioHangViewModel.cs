using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhuKienDienThoai.Models.SanPhamViewModels
{
    public class GioHangViewModel
    {
        public SanPham SanPham { get; set; }
        [Required(ErrorMessage="Số lượng là bắt buộc")]
        [Range(1,10, ErrorMessage="Số lượng phải lớn hơn 0 và nhỏ hơn 10")]
        public int SoLuong { get; set; }
        public int ThanhTien
        {
            get { return SanPham.DonGia * SoLuong; }
        }
    }
}