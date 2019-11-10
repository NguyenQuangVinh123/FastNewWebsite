namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Comment")]
    public partial class Comment
    {
        public int CommentID { get; set; }

        [Column(TypeName = "ntext")]
        public string ContentDetail { get; set; }

        public int UserID { get; set; }

        public int PostID { get; set; }

        public DateTime DateTimeCreate { get; set; }
    }
}
