using System.ComponentModel;

namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Role")]
    public partial class Role
    {
        public int RoleID { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc")]
        [StringLength(50)]
        [DisplayName("Tên quyền")]
        public string RoleName { get; set; }

        [DisplayName("Trạng thái")]
        public bool IsDisable { get; set; }
    }
}
