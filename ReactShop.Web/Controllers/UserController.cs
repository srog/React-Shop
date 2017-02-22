using System.Web.Mvc;
using ReactShop.Core;
using ReactShop.Core.Common;
using ReactShop.Core.Data.Customers;
using ReactShop.Core.DTOs;
using ReactShop.Core.Entities;
using ReactShop.Core.Enums;

namespace ReactShop.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ISaveCustomer _saveCustomer;
        private readonly IGetCustomer _getCustomer;
        private readonly IDeleteCustomerAddress _deleteCustomerAddress;

        public UserController()
        {
            _saveCustomer = AutoFacHelper.Resolve<ISaveCustomer>();
            _getCustomer = AutoFacHelper.Resolve<IGetCustomer>();
            _deleteCustomerAddress = AutoFacHelper.Resolve<IDeleteCustomerAddress>();
        }

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

        public ActionResult CreateAccount()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        [ValidateAntiForgeryToken]
        public ActionResult AddCustomerAccount(CustomerDTO cust)
        {
            if (ValidateUser(cust))
            {
                var newCustomerId = _saveCustomer.Save(new CustomerDTO
                {
                    Email = cust.Email,
                    Telephone = cust.Telephone,
                    ForeName = cust.ForeName,
                    Surname = cust.Surname,
                    Title = cust.Title,
                    Username = cust.Username,
                    Password = cust.Password,
                    Status = CustomerStatusEnum.Active,
                    Addresses = null
                });

                return RedirectToAction("Index", "Home");

            }
            return RedirectToAction("CreateAccount", "User");
        }

        private bool ValidateUser(CustomerDTO customerDto)
        {

            if (customerDto.Username == "" || customerDto.Password == "")
            {
                return false;
            }
            if (customerDto.Password != customerDto.PasswordConfirm)
            {
                return false;
            }

            if (_getCustomer.UsernameExists(customerDto.Username))
            {
                return false;
            }

            return true;
                
        }

        public ActionResult ManageAccount()
        {
            var customer = _getCustomer.GetById(Identity.LoggedInUserId);

            return View(CustomerDTO.FromCustomer(customer));
        }

        public ActionResult RemoveAddress(int addressid)
        {
            var address = CustomerAddress.FromDto(_getCustomer.GetCustomerAddressById(addressid));
            var customerId = address.CustomerId;
            _deleteCustomerAddress.Delete(address);

            var customerDTO = CustomerDTO.FromCustomer(_getCustomer.GetById(customerId));
            return View("ManageAccount", customerDTO);
        }

        public ActionResult AddAddress()
        {
            throw new System.NotImplementedException();
        }

        public ActionResult EditAddress(int addressid)
        {
            throw new System.NotImplementedException();
        }
    }
}