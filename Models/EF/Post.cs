using System.ComponentModel;

namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Post")]
    public partial class Post
    {
        public int PostID { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc")]
        [StringLength(1000)]
        [DisplayName("Tiêu đề bài viết")]
        public string Title { get; set; }

        [StringLength(1000)]
        public string MetaTitle { get; set; }

        [Required(ErrorMessage = "Dữ liệu bắt buộc")]
        [StringLength(1000)]
        [DisplayName("Mô tả ngắn")]
        public string Decription { get; set; }

        //[Required(ErrorMessage = "Dữ liệu bắt buộc")]
        [StringLength(250)]
        [DisplayName("Ảnh hiển thị đại điện")]
        public string Image { get; set; }

        //[Required(ErrorMessage = "Dữ liệu bắt buộc")]
        [Column(TypeName = "ntext")]
        [DisplayName("Nội dung chi tiết")]
        public string ContentDetail { get; set; }

        [DisplayName("Thể loại")]
        public int CategoryID { get; set; }

        public DateTime DatetimeCreate { get; set; }
    }
}
