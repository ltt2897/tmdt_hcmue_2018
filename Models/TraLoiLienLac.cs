using System;

namespace QuanLyBanSach.Models
{
    public class TraLoiLienLac
    {
        public int Id { get; set; }
        public LienLac LienLac { get; set; }
        public string TieuDe { get; set; }
        public string NoiDung { get; set; }
        public DateTime NgayTraLoi { get; set; } = DateTime.Now;
    }
}