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
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "mật khẩu mới")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "xác nhận mật khẩu mới")]
        [Compare("NewPassword", ErrorMessage = "xác nhận mật khẩu không trùng khớp.")]
        public string ConfirmPassword { get; set; }

        public string StatusMessage { get; set; }
    }
}
