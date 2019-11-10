using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Models.DAO;

namespace FastNews.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult CategoryDetail(int categoryID, int page = 1, int pageSize = 3)
        {
            int totalRecord = 0;
            var category = new PostDAO().GetDetailCategoryList(categoryID, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 10;
            int totalPage = (int)Math.Ceiling((double)(totalRecord * 1.0 / pageSize));

            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;

            return View(category);
        }
    }
}