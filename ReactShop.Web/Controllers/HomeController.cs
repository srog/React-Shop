using ReactShop.Core;
using System.Web.Mvc;
using ReactShop.Core.Data.Cart;
using ReactShop.Core.Data.Orders;
using ReactShop.Core.Data.Products;

namespace ReactShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGetProducts _getProducts;
        private readonly IGetCart _getCart;
        private readonly ICreateOrder _createOrder;
        private readonly ICheckoutManager _checkoutManager;

        public HomeController()
        {
            _getProducts = AutoFacHelper.Resolve<IGetProducts>();
            _getCart = AutoFacHelper.Resolve<IGetCart>();
            _createOrder = AutoFacHelper.Resolve<ICreateOrder>();
            _checkoutManager = AutoFacHelper.Resolve<ICheckoutManager>();
        }
        public HomeController(ICheckoutManager checkoutManager, 
            ICreateOrder createOrder,
            IGetProducts getProducts,
            IGetCart getCart)
        {
            
        }
        

        public ActionResult Index()
        {
            return View(_getProducts.Get());
        }

        public ActionResult Cart()
        {
            return View(_getCart.Get(null));
        }

        public ActionResult CheckoutSuccess()
        {
            _createOrder.Create(_getCart.Get(null));
            return View(_checkoutManager.GetCheckoutSummary());
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCart(string SKU)
        {
            //_saveCart.Save(new Core.DTOs.CartItemDTO
            //{
            //     SKU = SKU,
            //     Quantity = 1
            //});
            return RedirectToAction("Cart", "Home");
        }

     
        public PartialViewResult Details(string sku)
        {
            var productDetail = _getProducts.GetBySku(sku);
            return PartialView("_Details", productDetail);

        }
    }
}

