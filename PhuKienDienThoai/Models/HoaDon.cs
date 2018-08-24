using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PhuKienDienThoai.Models
{
    public class HoaDon
    {
        public int Id { get; set; }
        public DateTime NgayLapHoaDon { get; set; }
        [StringLength(50)]
        public string DiaChi { get; set; }
        public string GhiChu { get; set; }
        //pivot trạng thái, nếu ngày giao là null thì trạng thái là chưa giao
        //nếu ngày giao có ngày tháng thì trạng thái đã giao
        public DateTime? NgayGiao { get; set; }
        [StringLength(255)]
        public string PhuongThucThanhToan { get; set; }
        public int TongThanhTien { get; set; }
        public List<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public ApplicationUser User { get; set; }
        public HoaDon()
        {
            NgayLapHoaDon = DateTime.Now;
            ChiTietHoaDons = new List<ChiTietHoaDon>();
            User = new ApplicationUser();
        }
    }
}