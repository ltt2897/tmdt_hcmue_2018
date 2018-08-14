using System;

namespace QuanLyBanSach.Models
{
    public class ChiTietHoaDon
    {
        public int HoaDonId { get; set; }
        public HoaDon HoaDon { get; set; }
        public int SachId { get; set; }
        public Sach Sach { get; set; }
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