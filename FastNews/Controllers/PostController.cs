using System;
using System.Runtime.Remoting.Contexts;
using System.Web.Mvc;

using FastNews.Common;

using Microsoft.Owin.Security.Facebook;

using Models.DAO;
using Models.EF;

namespace FastNews.Controllers
{
    public class PostController : Controller
    {
        public ActionResult PostDetail(int postID)
        {
            ViewBag.SimilarPost = new PostDAO().GetSimmilarPost(postID);
            ViewBag.Comment = new PostDAO().GetRecentComment(postID);

            ViewBag.PreviousPost = new PostDAO().GetPrevOrNextPost(postID, "PREV");
            ViewBag.NextPost = new PostDAO().GetPrevOrNextPost(postID, "NEXT");

            var post = new PostDAO().ViewDetail(postID);
            return View(post);
        }

        [HttpPost]
        public ActionResult InsertComment(string commentDetail, int postID, string url)
        {
            if (commentDetail.Replace(" ", "") == string.Empty)
            {
                ViewBag.ErrorComment = "Bình luận chưa có nội dung.";
            }
            else
            {
                var session = (UserLogin)Session[Commonstants.USER_SESSION];
                var comment = new Comment();
                comment.ContentDetail = commentDetail;
                comment.UserID = session.UserID;

                comment.PostID = postID;
                comment.DateTimeCreate = DateTime.Now;
                var result = new CommentDAO().Insert(comment);
                if (result <= 0)
                {
                    ViewBag.ErrorComment = "Thao tác không thành công.";
                }
            }
           
            return Redirect(url);
        }
         
        public ActionResult Search(string keyword, int page = 1, int pageSize = 3)
        {
            int totalRecord = 0;
            var post = new PostDAO().Search(keyword, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 10;
            int totalPage = (int)Math.Ceiling((double)(totalRecord * 1.0 / pageSize));

            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Keyword = keyword;
            return View(post);
        }
    }
}