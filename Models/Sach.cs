using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyBanSach.Models
{
    public class Sach
    {
        public int id { get; set; }
        [StringLength(255)]
        public string TenSach { get; set; }
        public int DonGia { get; set; }
        public int ChieuDai { get; set; }
        public int ChieuRong { get; set; }
        public  TacGia TacGia { get; set; }
        [ForeignKey("TacGia")]
        public int TacGiaId { get; set; }
        [ForeignKey("NhaXuatBan")]
        public int NhaXuatBanId { get; set; }
        public  NhaXuatBan NhaXuatBan { get; set; }
        [ForeignKey("DanhMucId")]
        public int DanhMucId { get; set; }
        public  DanhMuc DanhMuc { get; set; }
        [StringLength(100)]
        public string DinhDang { get; set; }
        [StringLength(200)]
        public string HinhAnh { get; set; }
        public ChuDe ChuDe { get; set; }
        [ForeignKey("ChuDe")]
        public int ChuDeId { get; set; }
        public int PhanTramGiamGia { get; set; }
        public int SoLuong { get; set; }
        public int SoTrang { get; set; }
        public string TomTat { get; set; }
        public List<NhanXet> DanhGias { get; set; }
        public List<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public List<Wishlist> Wishlist { get; set; }
        public Sach()
        {
            //Định dạng mặc định bằng bìa mềm
            DinhDang = "Bìa mềm";
            //mặc định chưa có cuốn sách nào được giảm giá
            PhanTramGiamGia = 0;
            //số lượng mặc định bằng 200
            SoLuong = 200;
            //danh sách các đánh giá
            DanhGias = new List<NhanXet>();
            //danh sách wishlist
            Wishlist = new List<Wishlist>();
            ChiTietHoaDons = new List<ChiTietHoaDon>();
            //khởi tạo giá trị
            TacGia = new TacGia();
            NhaXuatBan = new NhaXuatBan();
            DanhMuc = new DanhMuc();
        }
    }
}