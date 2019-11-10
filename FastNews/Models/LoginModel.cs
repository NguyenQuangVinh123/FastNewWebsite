using System;
using System.ComponentModel.DataAnnotations;

namespace FastNews.Models
{
    public class LoginModel
    {
        [Key]
        [Required(ErrorMessage = "Dữ liệu bắt buộc")]
        public string AccountName { set; get; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc")]
        public string Password { set; get; }
    }
}