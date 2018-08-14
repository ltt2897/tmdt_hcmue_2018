using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Collections.Generic;
using QuanLyBanSach.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace QuanLyBanSach.Areas.Admin.Models.SachViewModels
{
    public class SuaSachViewModel
    {
        #region id
        public int Id { get; set; }
        #endregion

        #region Tên sách
        [Display(Name = "Tên sách")]
        [Required(ErrorMessage = "Tên sách là bắt buộc")]
        public string TenSach { get; set; }

        #endregion

        #region Đơn giá
        [Display(Name = "Đơn giá")]
        [Required(ErrorMessage = "Đơn giá là bắt buộc")]
        public int DonGia { get; set; }
        #endregion

        #region Chiều dài

        [Display(Name = "Chiều Dài")]
        [Required(ErrorMessage = "Chiều dài sách là bắt buộc")]
        public int ChieuDai { get; set; }
        #endregion

        #region Chiều rộng
        [Display(Name = "Chiều rộng")]
        [Required(ErrorMessage = "Chiều rộng là bắt buộc")]
        public int ChieuRong { get; set; }
        #endregion

        #region tác giả

        [Display(Name = "Tác giả")]
        public int TacGiaId { get; set; }
        public List<TacGia> TacGias { get; set; }

        #endregion

        #region Nhà xuất bản

        [Display(Name = "Nhà Xuất bản")]
        public int NhaXuatBanId { get; set; }
        public List<NhaXuatBan> NhaXuatBans { get; set; }
        #endregion

        #region Danh mục

        [Display(Name = "Danh mục")]
        public int DanhMucId { get; set; }
        public List<DanhMuc> DanhMucs { get; set; }

        #endregion

        #region Định dạng

        [Display(Name = "Định dạng")]
        [Required(ErrorMessage = "Định dạng bắt buộc")]
        public string DinhDang { get; set; }
        #endregion

        #region chủ đề

        [Display(Name = "Chủ Đề")]
        public int ChuDeId { get; set; }
        public List<ChuDe> ChuDes { get; set; }

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
        public int SoTrang { get; set; }

        #endregion

        #region Tóm tắt
            
        
        [Display(Name = "Tóm tắt")]
        public string TomTat { get; set; }
        #endregion

        #region Hình ảnh
            
        [Display(Name = "Hình Ảnh")]

        [DataType(DataType.Upload)]
        public IFormFile uploadHinhAnh { get; set; }
        public string HinhAnh { get; set; }
        #endregion
    }
}