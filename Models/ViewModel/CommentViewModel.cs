using System;

using Models.EF;

namespace Models.ViewModel
{
    public class CommentViewModel
    {
        public string Username { get; set; }
        public string ContentDetail { get; set; }
        public DateTime DateTimeCreate { get; set; }
    }
}
