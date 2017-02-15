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

        public ActionResult Login(string customerId)
        {
            Identity.Login(customerId);
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Identity.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}