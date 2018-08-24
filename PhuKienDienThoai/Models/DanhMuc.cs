using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhuKienDienThoai.Models
{
    public class DanhMuc
    {
        public int Id { get; set; }
        public string TenDanhMuc { get; set; }
        [ForeignKey("MatHang")]
        public int MatHangId { get; set; }
        public ICollection<SanPham> SanPhames { get; set; }
        public MatHang MatHang { get; set; }
        public DanhMuc()
        {
            SanPhames = new HashSet<SanPham>();
        }
    }
}