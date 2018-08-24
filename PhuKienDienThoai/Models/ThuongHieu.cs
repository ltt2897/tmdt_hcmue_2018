using System.Collections.Generic;

namespace PhuKienDienThoai.Models
{
    public class ThuongHieu
    {
        public int Id { get; set; }
        public string TenThuongHieu { get; set; }
        public ICollection<SanPham> SanPhames { get; set; }
        public ThuongHieu()
        {
            SanPhames = new HashSet<SanPham>();
        }
    }
}