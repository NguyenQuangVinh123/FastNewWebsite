using System.ComponentModel;

namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        public int AccountID { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc")]
        [DisplayName("Tên tài khoản")]
        [StringLength(50)]
        public string AccountName { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc")]
        [DisplayName("Mật khẩu")]
        [StringLength(50)]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc")]
        [DisplayName("Tên người dùng")]
        [StringLength(50)]
        public string Name { get; set; }

        [DisplayName("Mã quyền")]
        public int RoleID { get; set; }

        [DisplayName("Trạng thái")]
        public bool IsLock { get; set; }
    }
}
