using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLyBanSach.Models
{
    public class DanhMuc
    {
        public int Id { get; set; }
        public string TenDanhMuc { get; set; }
        public ICollection<Sach> Saches { get; set; }
        public DanhMuc()
        {
            Saches = new HashSet<Sach>();
        }
    }
}