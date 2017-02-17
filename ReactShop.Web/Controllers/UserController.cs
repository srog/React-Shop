using System.Web.Mvc;
using ReactShop.Core.Common;

namespace ReactShop.Web.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string txtusername, string txtpassword)
        {
            if (Identity.Login(txtusername, txtpassword))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("LoginFail", "User");
            }
        }

        public ActionResult Logout()
        {
            Identity.Logout();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult LoginFail()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}