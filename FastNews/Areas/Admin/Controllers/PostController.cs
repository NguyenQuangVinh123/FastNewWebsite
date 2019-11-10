using Models.DAO;
using Models.EF;
using System;
using System.Web.Mvc;

using FastNews.Common;

namespace FastNews.Areas.Admin.Controllers
{
    public class PostController : BaseController
    {
        // GET: Admin/Post
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new PostDAO();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        public void SetViewBag(long? selectedId = null)
        {
            var dao = new CategoryDAO();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "CategoryID", "CategoryName", selectedId);
        }

        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Post post)
        {
            if (ModelState.IsValid)
            {
                var dao = new PostDAO();
                post.MetaTitle = ConvertToUnSign.utf8Convert(post.Title);
                post.DatetimeCreate = DateTime.Now;

                long id = dao.Insert(post);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Post");

                }
                else
                {
                    ModelState.AddModelError("", "Thêm không thành công");
                }
            }
            SetViewBag();
            return View(post);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new PostDAO().Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var post = new PostDAO().ViewDetail(id);
            SetViewBag(post.CategoryID);
            return View(post);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                var dao = new PostDAO();

                // chuyen sang chuoi khong dau
                // gan vao cho metatitle
                post.MetaTitle = ConvertToUnSign.utf8Convert(post.Title);

                var result = dao.Update(post);
                if (result)
                {
                    return RedirectToAction("Index", "Post");

                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            SetViewBag(post.CategoryID);
            return View(post);
        }
    }
}