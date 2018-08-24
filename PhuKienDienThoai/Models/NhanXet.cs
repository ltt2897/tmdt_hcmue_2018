using System;
using System.ComponentModel.DataAnnotations;

namespace PhuKienDienThoai.Models
{
    public class NhanXet
    {
        [Key]
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public virtual SanPham SanPham { get; set; }
        [StringLength(50)]
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public DateTime NgayDang { get; set; }

        public NhanXet()
        {
            SanPham = new SanPham();
            NgayDang = DateTime.Now;
        }
    }
}