using System.ComponentModel;

namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        public int CategoryID { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc")]
        [StringLength(50)]
        [DisplayName("Tên thể loại")]
        public string CategoryName { get; set; }

        [StringLength(50)]
        public string MetaTitle { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc")]
        [DisplayName("Thứ tự hiển thị")]
        public int DisplayOrder { get; set; }

        [StringLength(10)]
        [DisplayName("Cách điều hướng")]
        public string Target { get; set; }

        [DisplayName("Hiển thị trên trang chủ")]
        public bool ShowOnHome { get; set; }

        [DisplayName("Hiển thị trên menu")]
        public bool ShowOnMenu { get; set; }
    }
}
