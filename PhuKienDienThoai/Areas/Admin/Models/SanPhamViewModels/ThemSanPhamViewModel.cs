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

        #region Dòng điện thoại

        [Display(Name = "Dòng điện thoại")]
        [Required(ErrorMessage = "Vui lòng chọn Dòng điện thoại")]
        public int DongDienThoaiId { get; set; }
        public List<DongDienThoai> DongDienThoais { get; set; }

        #endregion

        #region Thương hiệu

        [Display(Name = "Thương hiệu")]
        [Required(ErrorMessage = "vui lòng chọn Thương hiệu")]
        public int ThuongHieuId { get; set; }
        public List<ThuongHieu> ThuongHieus { get; set; }
        #endregion

        #region Danh mục

        [Display(Name = "Danh mục")]
        [Required(ErrorMessage = "Vui lòng chọn Danh mục")]
        public int DanhMucId { get; set; } = 1;
        public List<DanhMuc> DanhMucs { get; set; }

        #endregion

        #region Chất liệu

        [Display(Name = "Chất liệu")]
        [Required(ErrorMessage = "Chất liệu bắt buộc")]
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

        #region Màu sắc
        [Required(ErrorMessage = "Màu sắc là bắt buộc")]
        [Display(Name = "Màu sắc")]
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
    }
}