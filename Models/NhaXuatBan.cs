using System.Collections.Generic;

namespace QuanLyBanSach.Models
{
    public class NhaXuatBan
    {
        public int Id { get; set; }
        public string TenNhaXuatBan { get; set; }
        public string SoDienThoai { get; set; }
        public ICollection<Sach> Saches { get; set; }
        public NhaXuatBan()
        {
            Saches = new HashSet<Sach>();
        }
    }
}