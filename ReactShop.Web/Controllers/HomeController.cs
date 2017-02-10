using ReactShop.Core;
using System.Web.Mvc;
using ReactShop.Core.Entities;

namespace ReactShop.Web.Controllers
{
    public class HomeController : Controller
    {
        readonly ICheckoutManager checkoutManager;

        public HomeController()
        {
            this.checkoutManager = AutoFacHelper.Resolve<ICheckoutManager>();
        }

        public HomeController(ICheckoutManager checkoutManager)
        {
            this.checkoutManager = checkoutManager;
        }

        public ActionResult Index()
        {
            return View(checkoutManager.GetProducts());
        }

        public ActionResult Cart()
        {
            return View(checkoutManager.GetCart());
        }

        public ActionResult CheckoutSuccess()
        {
            checkoutManager.CreateOrder(checkoutManager.GetCart());
            return View(checkoutManager.GetCheckoutSummary());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCart(string SKU)
        {
            checkoutManager.SaveCart(new Core.DTOs.CartItemDTO
            {
                 SKU = SKU,
                 Quantity = 1
            });
            return RedirectToAction("Cart", "Home");
        }

     
        public PartialViewResult Details(string sku)
        {
            var productDetail = checkoutManager.GetProduct(sku);
            return PartialView("_Details", productDetail);

        }
    }
}

