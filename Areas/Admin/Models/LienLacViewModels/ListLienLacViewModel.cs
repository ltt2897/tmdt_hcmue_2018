using System;

namespace PhuKienDienThoai.Areas.Admin.Models.LienLacViewModels
{
    public class ListLienLacViewModel
    {
        public int Id { get; set; }
        public string TieuDe { get; set; }
        public DateTime NgayGoi { get; set; }
        public bool isRead { get; set; }
    }
}