using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PhuKienDienThoai.Models.ManageViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu cũ")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Mật khẩu mới phải tối thiếu {2} và tối đa {1} kí tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu mới")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu mới")]
        [Compare("NewPassword", ErrorMessage = "Xác nhận mật khẩu mới không trùng khớp.")]
        public string ConfirmPassword { get; set; }

        public string StatusMessage { get; set; }
    }
}
