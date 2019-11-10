using Models.DAO;
using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using FastNews.Common;

namespace FastNews.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        // GET: Admin/Category
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new CategoryDAO();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category cate)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDAO();
                cate.MetaTitle = ConvertToUnSign.utf8Convert(cate.CategoryName);
                long id = dao.Insert(cate);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Category");

                }
                else
                {
                    ModelState.AddModelError("", "Thêm không thành công");
                }
            }
            return View(cate);
        }


        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = new CategoryDAO().Delete(id);
            if (result == 0)
            {
                ModelState.AddModelError("", "Xóa thất bại");
            }
            else if(result == -1)
            {
                    ModelState.AddModelError("", "Không thể xóa được, bài viết thuộc thể loại này vẫn còn tồn tại");
            }

            return  RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var cate = new CategoryDAO().ViewDetail(id);
            return View(cate);
        }

        [HttpPost]
        public ActionResult Edit(Category cate)
        {
            if (ModelState.IsValid)
            {
                var dao = new CategoryDAO();

                // chuyen thanh chuoi khong dau 
                // gan cho field Metatile 
                cate.MetaTitle = ConvertToUnSign.utf8Convert(cate.CategoryName);

                var result = dao.Update(cate);
                if (result)
                {
                    return RedirectToAction("Index", "Category");

                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            return View(cate);

        }
    }
}