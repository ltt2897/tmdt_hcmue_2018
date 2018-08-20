using System;
using System.ComponentModel.DataAnnotations;

namespace PhuKienDienThoai.Models
{
    public class LienLac
    {
        public int Id { get; set; }
        [StringLength(50)]
        public string Ten { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(200)]
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public DateTime NgayGoi { get; set; } = DateTime.Now;
       
    }
}