using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLyBanSach.Models
{
    public class NhanXet
    {
        [Key]
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public virtual Sach Sach { get; set; }
        [StringLength(50)]
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public DateTime NgayDang { get; set; }

        public NhanXet()
        {
            Sach = new Sach();
            NgayDang = DateTime.Now;
        }
    }
}