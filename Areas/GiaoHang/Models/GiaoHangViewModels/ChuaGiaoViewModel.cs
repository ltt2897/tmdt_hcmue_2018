using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuanLyBanSach.Data;

namespace QuanLyBanSach.Areas.GiaoHang.Models.GiaoHangViewModels
{
    public class ChuaGiaoViewModel
    {
        public string Id { get; set; } = "1";
        public List<SelectListItem> DanhSachHoaDon { get; set; }
        public ChuaGiaoViewModel() => DanhSachHoaDon = new List<SelectListItem>();
    }
}