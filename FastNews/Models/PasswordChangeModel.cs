using System.ComponentModel.DataAnnotations;
using Models.EF;

namespace FastNews.Models
{
    public class PasswordChangeModel : Account 
    {
        [Required(ErrorMessage = "Dữ liệu bắt buộc")]
        [Display(Name = "Mật khẩu mới")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc")]
        [Display(Name = "Nhập lại mật khẩu mới")]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu xác nhận phải trùng với mật khẩu mới")]
        public string ConfirmNewPassword { get; set; }
    }
}