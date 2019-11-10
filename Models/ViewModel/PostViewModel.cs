using System.Collections.Generic;
using Models.EF;

namespace Models.ViewModel
{
    public class PostViewModel
    {
        public string CategoryName { get; set; }
        public string CategoryMetaTitle { get; set; }

        public List<Post> ListPosts { get; set; }
    }
}
