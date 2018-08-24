using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhuKienDienThoai.Models
{
    public class SanPham
    {
        public int id { get; set; }
        [StringLength(255)]
        public string TenSanPham { get; set; }
        public int DonGia { get; set; }
        public  DongDienThoai DongDienThoai { get; set; }
        [ForeignKey("DongDienThoai")]
        public int DongDienThoaiId { get; set; }
        [ForeignKey("ThuongHieu")]
        public int ThuongHieuId { get; set; }
        public  ThuongHieu ThuongHieu { get; set; }
        [ForeignKey("DanhMucId")]
        public int DanhMucId { get; set; }
        public  DanhMuc DanhMuc { get; set; }
        [StringLength(100)]
        public string DinhDang { get; set; }
        [StringLength(200)]
        public string HinhAnh { get; set; }
        public MatHang MatHang { get; set; }
        [ForeignKey("MatHang")]
        public int MatHangId { get; set; }
        public int PhanTramGiamGia { get; set; }
        public int SoLuong { get; set; }
        public string MauSac { get; set; }
        public string TomTat { get; set; }
        public List<NhanXet> DanhGias { get; set; }
        public List<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public List<Wishlist> Wishlist { get; set; }
        public SanPham()
        {
            //Chất liệu mặc định bằng bìa mềm
            DinhDang = "Nhựa";
            //mặc định chưa có sản phẩm nào được giảm giá
            PhanTramGiamGia = 0;
            //số lượng mặc định bằng 200
            SoLuong = 200;
            //danh sách các đánh giá
            DanhGias = new List<NhanXet>();
            //danh sách wishlist
            Wishlist = new List<Wishlist>();
            ChiTietHoaDons = new List<ChiTietHoaDon>();
            //khởi tạo giá trị
            DongDienThoai = new DongDienThoai();
            ThuongHieu = new ThuongHieu();
            DanhMuc = new DanhMuc();
            MatHang = new MatHang();
        }
    }
}