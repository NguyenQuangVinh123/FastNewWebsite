using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace FastNews.Areas.Admin.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Mời nhập User Name")]
        public string UserName { set; get; }

        [Required(ErrorMessage = "Mời nhập mật khẩu")]
        public string PassWord { set; get; }
    }
}