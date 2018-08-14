
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLyBanSach.Models
{
    public class TacGia
    {
        [Key]
        public int Id { get; set; }
        public string TenTacGia { get; set; }
        public ICollection<Sach> Saches { get; set; }
        public TacGia()
        {
            Saches= new HashSet<Sach>();
        }
    }
}