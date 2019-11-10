using System.Web.Mvc;
using FastNews.Common;

using Models.DAO;

namespace FastNews.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string exceptHome = Commonstants.META_TITLE_HOME.ToLower();

            ViewBag.PostTest = new PostDAO().GetTop3Post(exceptHome);
            ViewBag.LeftSlide = new PostDAO().GetPostForSlide("left");
            ViewBag.RightSlide = new PostDAO().GetPostForSlide("right");
            return View();
        }

        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            var menu = new CategoryDAO().ShowMenuCategory();
            return PartialView(menu);
        }


        [ChildActionOnly]
        public ActionResult RecentPost()
        {
            var recent = new PostDAO().GetRecentPost();
            return PartialView(recent);
        }
    }
}