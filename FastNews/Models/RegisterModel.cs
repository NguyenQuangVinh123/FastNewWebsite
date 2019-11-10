using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Models.EF;

namespace FastNews.Models
{
    public class RegisterModel : Account
    {
        [Required(ErrorMessage = "Dữ liệu bắt buộc")]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Xác nhận phải trùng với mật khẩu.")]
        public string ConfirmPassword { get; set; }
    }
}