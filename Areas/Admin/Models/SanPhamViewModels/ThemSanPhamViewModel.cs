using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Collections.Generic;
using PhuKienDienThoai.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using PhuKienDienThoai.Data;

namespace PhuKienDienThoai.Areas.Admin.Models.SanPhamViewModels
{
    public class ThemSanPhamViewModel
    {
        #region Tên sản phẩm
        [Display(Name = "Tên sản phẩm")]
        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc")]
        public string TenSanPham { get; set; }

        #endregion

        #region Đơn giá
        [Display(Name = "Đơn giá")]
        [Required(ErrorMessage = "Đơn giá là bắt buộc")]
        public int DonGia { get; set; }
        #endregion

        #region tác giả

        [Display(Name = "Tác giả")]
        [Required(ErrorMessage = "Vui lòng chọn tác giả")]
        public int DongDienThoaiId { get; set; }
        public List<DongDienThoai> DongDienThoais { get; set; }

        #endregion

        #region Thương hiệu

        [Display(Name = "Thương hiệu")]
        [Required(ErrorMessage = "vui lòng chọn nhà xuất bạn")]
        public int ThuongHieuId { get; set; }
        public List<ThuongHieu> ThuongHieus { get; set; }
        #endregion

        #region Danh mục

        [Display(Name = "Danh mục")]
        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        public int DanhMucId { get; set; } = 1;
        public List<DanhMuc> DanhMucs { get; set; }

        #endregion

        #region Định dạng

        [Display(Name = "Định dạng")]
        [Required(ErrorMessage = "Định dạng bắt buộc")]
        public string DinhDang { get; set; }
        #endregion

        #region Mặt hàng

        [Display(Name = "Mặt hàng")]
        [Required(ErrorMessage = "Vui lòng chọn mặt hàng")]
        public int MatHangId { get; set; }
        public List<MatHang> MatHangs { get; set; }

        #endregion

        #region Phần trăm giảm giá

        [Display(Name = "Phần trăm giảm giá")]
        public int PhanTramGiamGia { get; set; } = 0;

        #endregion

        #region Số lượng

        [Display(Name = "Số lượng")]
        [Required(ErrorMessage = "Số lượng là bắt buộc")]
        public int SoLuong { get; set; }

        #endregion

        #region Số trang
        [Required(ErrorMessage = "Số trang là bắt buộc")]
        [Display(Name = "Số trang")]
        public string MauSac { get; set; }
        #endregion

        #region Tóm tắt
        [Display(Name = "Tóm tắt")]
        public string TomTat { get; set; }
        #endregion

        #region Hình ảnh
        [Display(Name = "Hình Ảnh")]
        [DataType(DataType.Upload)]
        [Required(ErrorMessage = "vui lòng upload hình ảnh")]
        public IFormFile uploadHinhAnh { get; set; }
        public string HinhAnh { get; set; }
        #endregion

        #region Contructor
        public ThemSanPhamViewModel(ApplicationDbContext context)
        {
            ThuongHieuId = context.ThuongHieu.Min(x => x.Id);
            MatHangId = context.MatHang.Min(x => x.Id);
            DanhMucId = context.DanhMuc.Min(x => x.Id);
            DongDienThoaiId = context.DongDienThoai.Min(x => x.Id);
            MatHangs = context.MatHang.ToList();
            DanhMucs = context.DanhMuc.ToList();
            ThuongHieus = context.ThuongHieu.ToList();
            DongDienThoais = context.DongDienThoai.ToList();
        }

        #endregion
    }
}