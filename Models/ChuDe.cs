using System.Collections.Generic;
using System.Linq;
namespace QuanLyBanSach.Models
{
    public class ChuDe
    {
        public int Id { get; set; }
        public string TenChuDe { get; set; }
        public ICollection<Sach> Saches { get; set; }
        public ChuDe()
        {
            Saches = new HashSet<Sach>();
        }
    }
}