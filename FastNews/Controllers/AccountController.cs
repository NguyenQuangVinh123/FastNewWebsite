using System.Web.Mvc;

using FastNews.Common;
using FastNews.Models;


using Models.DAO;
using Models.EF;

namespace FastNews.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session[Commonstants.USER_SESSION] = null;
            return Redirect("/");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult PasswordChange()
        {
            return View();
        }   

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new AccountDAO();
                var result = dao.Login(model.AccountName, Encryptor.MD5Hash(model.Password));
                if (result == 1 || result == -3)
                {
                    var user = dao.GetById(model.AccountName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.AccountName;
                    userSession.RoleID = user.RoleID;
                    userSession.UserID = user.AccountID;
                    Session.Add(Commonstants.USER_SESSION, userSession);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ViewBag.Error = "Tài khoản không tồn tại.";
                }
                else if (result == -1)
                {
                    ViewBag.Error = "Tài khoản đang bị khóa";
                }
                else if (result == -2)
                {
                    ViewBag.Error = "Mật khẩu không đúng.";
                }
                else
                {
                    ViewBag.Error = "Đăng nhập thất bại.";
                }
            }
            return View(model);
        }
        public ActionResult Thankyou()
        {
            TempData["alertMessage"] = "Whatever you want to alert the user with";
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var account = new Account();
                var encrytedMd5Pas = Encryptor.MD5Hash(model.Password);
                
                account.AccountName = model.AccountName;
                account.Password = encrytedMd5Pas;
                account.Name = model.Name;
                account.RoleID = 2;
                account.IsLock = false;

                long id = new AccountDAO().Insert(account);
                
                if (id > 0)
                {
                    TempData["msg"] = "<script>alert('Bạn đã đăng ký thành công');</script>";
                    return RedirectToAction("Index", "Home");

                }
                else if (id == -1)
                {
                    ViewBag.Error = "Tên tài khoản đã tồn tại";
                }
                else
                {
                    ViewBag.Error = "Thêm không thành công";
                }
                
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult PasswordChange(string newPassword, string confirmNewPassword, string userName)
        {
            if (ModelState.IsValid)
            {
                if (newPassword != confirmNewPassword)
                {
                    ViewBag.Error = "Xác nhận mật khẩu mới không đúng";
                    return View();
                }
                var account = new Account();

                var encrytedMd5Pas = Encryptor.MD5Hash(newPassword);

                account.AccountName = userName;
                account.Password = encrytedMd5Pas;

                int result = new AccountDAO().ChangePassword(account);

                if (result > 0)
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (result == -1)
                {
                    ViewBag.Error = "Mật khẩu mới không được trùng với mật khẩu đang sử dụng";
                }
                else
                {
                    ViewBag.Error = "Đổi mật khẩu thất bại";
                }
            }
            return View();
        }

    }
}