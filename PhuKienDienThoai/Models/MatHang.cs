using System.Collections.Generic;
using System.Linq;
namespace PhuKienDienThoai.Models
{
    public class MatHang
    {
        public int Id { get; set; }
        public string TenMatHang { get; set; }
        public ICollection<SanPham> SanPhames { get; set; }
        public ICollection<DanhMuc> DanhMuces { get; set; }
        public MatHang()
        {
            SanPhames = new HashSet<SanPham>();
            DanhMuces = new HashSet<DanhMuc>();
        }
    }
}