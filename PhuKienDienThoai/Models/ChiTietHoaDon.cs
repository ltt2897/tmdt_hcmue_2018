using System;

namespace PhuKienDienThoai.Models
{
    public class ChiTietHoaDon
    {
        public int HoaDonId { get; set; }
        public HoaDon HoaDon { get; set; }
        public int SanPhamId { get; set; }
        public SanPham SanPham { get; set; }
        public int SoLuong { get; set; }
        public int ThanhTien { get; set; }
        public DateTime NgayThem { get; set; }
        public ChiTietHoaDon()
        {
            HoaDon = new HoaDon();
            NgayThem = DateTime.Now;
        }
    }
}