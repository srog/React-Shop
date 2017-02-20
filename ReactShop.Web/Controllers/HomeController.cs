using System.Linq;
using ReactShop.Core;
using System.Web.Mvc;
using ReactShop.Core.Common;
using ReactShop.Core.Data.Cart;
using ReactShop.Core.Data.Orders;
using ReactShop.Core.Data.Products;
using ReactShop.Core.Entities;
using ReactShop.Core.Enums;

namespace ReactShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGetProducts _getProducts;
        private readonly IGetCart _getCart;
        private readonly IGetCartItem _getCartItem;
        private readonly ISaveCart _saveCart;
        private readonly ICreateOrder _createOrder;
        private readonly ICheckoutManager _checkoutManager;

        public HomeController()
        {
            _getProducts = AutoFacHelper.Resolve<IGetProducts>();
            _getCart = AutoFacHelper.Resolve<IGetCart>();
            _getCartItem = AutoFacHelper.Resolve<IGetCartItem>();
            _saveCart = AutoFacHelper.Resolve<ISaveCart>();

            _createOrder = AutoFacHelper.Resolve<ICreateOrder>();
            _checkoutManager = AutoFacHelper.Resolve<ICheckoutManager>();
        }
        
        public ActionResult Index()
        {
            return View(_getProducts.Get());
        }

        public ActionResult Cart()
        {
            return View(_getCart.Get(Identity.LoggedInUserId));
        }

        public ActionResult SearchBar(string searchText)
        {
            var results = _getProducts.Get()
                .Where(p => p.Description.ToLower().Contains(searchText.ToLower()) && p.Status == ProductStatusEnum.Live)
                .ToList();

            return View("Index", results);
        }

        public ActionResult CheckoutSuccess()
        {
            var orderId =_createOrder.Create(_getCart.Get(Identity.LoggedInUserId));
            return View(_checkoutManager.GetCheckoutSummary(orderId));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCart(string sku)
        {
            var product = _getProducts.GetBySku(sku);
            var cartList = _getCart.Get(Identity.LoggedInUserId);
            var existingcartItemDTO = cartList.CartItems.FirstOrDefault(ci => ci.ProductId == product.Id);
            if (existingcartItemDTO != null)
            {
                var cartItem = _getCartItem.GetById(existingcartItemDTO.Id);
                cartItem.Quantity++;
                _saveCart.Save(cartItem);
            }
            else
            {
                _saveCart.Save(new CartItem
                {
                    CustomerId = Identity.LoggedInUserId,
                    Quantity = 1,
                    ProductId = product.Id
                });
            }

            return RedirectToAction("Cart", "Home");
        }
        
        public PartialViewResult Details(string sku)
        {
            var productDetail = _getProducts.GetBySku(sku);
            return PartialView("_Details", productDetail.ToProduct());
        }

        [HttpGet]
        public ActionResult DoLogin()
        {
            return PartialView("LoginPopup");
        }
    }
}

