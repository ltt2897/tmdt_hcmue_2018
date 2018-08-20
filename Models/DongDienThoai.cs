
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhuKienDienThoai.Models
{
    public class DongDienThoai
    {
        [Key]
        public int Id { get; set; }
        public string TenDongDienThoai { get; set; }
        public ICollection<SanPham> SanPhames { get; set; }
        public DongDienThoai()
        {
            SanPhames= new HashSet<SanPham>();
        }
    }
}