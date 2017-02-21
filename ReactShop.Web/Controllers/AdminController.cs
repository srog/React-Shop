using System.Web.Mvc;
using ReactShop.Core;
using ReactShop.Core.Data.Customers;
using ReactShop.Core.Data.Orders;
using ReactShop.Core.Data.Products;

namespace ReactShop.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IGetOrders _getOrders;
        private readonly IGetCustomer _getCustomers;
        private readonly IGetProducts _getProducts;
        public AdminController()
        {
            _getOrders = AutoFacHelper.Resolve<IGetOrders>();
            _getCustomers = AutoFacHelper.Resolve<IGetCustomer>();
            _getProducts = AutoFacHelper.Resolve<IGetProducts>();

        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewOrders()
        {
            var orderList = _getOrders.GetAll();
            return View(orderList);
        }

        public ActionResult ViewCustomers()
        {
            var customerList = _getCustomers.GetAll();
            return View(customerList);
        }

        public ActionResult ViewProducts()
        {
            var productList = _getProducts.GetAll();
            return View(productList);
        }
    }
}