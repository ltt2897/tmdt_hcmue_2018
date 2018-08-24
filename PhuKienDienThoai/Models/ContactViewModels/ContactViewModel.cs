using System.ComponentModel.DataAnnotations;

namespace PhuKienDienThoai.Models.ContactViewModels
{
    public class ContactViewModel
    {
        [Required(ErrorMessage="Tên không được trống")]
        [StringLength(maximumLength:50, ErrorMessage="Tên chỉ chứa được 50 ký tự")]
        public string Ten { get; set; }
        [Required(ErrorMessage="Email không được để trống")]
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(200)]
        [Required(ErrorMessage="tiêu đề không được để trống")]
        public string TieuDe { get; set; }
        [Required(ErrorMessage="Nội dung không được để trống")]
        public string NoiDung { get; set; }
    }
}