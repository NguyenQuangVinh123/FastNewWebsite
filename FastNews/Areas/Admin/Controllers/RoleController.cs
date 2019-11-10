using Models.DAO;
using Models.EF;
using System.Web.Mvc;

namespace FastNews.Areas.Admin.Controllers
{
    public class RoleController : Controller
    {
        // GET: Admin/Role
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new RoleDAO();
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
        public ActionResult Create(Role role)
        {
                if (ModelState.IsValid)
                {
                    var dao = new RoleDAO();
                    long id = dao.Insert(role);
                    if (id > 0)
                    {
                        return RedirectToAction("Index", "Role");

                    }
                    else if (id == -1)
                    {
                        ModelState.AddModelError("", "Tên quyền đã tồn tại");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm không thành công");
                    }
                }
            return View(role);
        }


        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new RoleDAO().Delete(id);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var role = new RoleDAO().ViewDetail(id);
            return View(role);
        }

        [HttpPost]
        public ActionResult Edit(Role role)
        {
            if (ModelState.IsValid)
            {
                var dao = new RoleDAO();
                var result = dao.Update(role);
                if (result == 1)
                {
                    return RedirectToAction("Index", "Role");

                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tên quyền này đã có trên hệ thống, vui lòng nhập tên khác!");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            return View(role);
        }
    }
}