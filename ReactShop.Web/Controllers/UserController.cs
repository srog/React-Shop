using System.Web.Http.Controllers;
using System.Web.Mvc;
using ReactShop.Core;
using ReactShop.Core.Common;
using ReactShop.Core.Data.Customers;
using ReactShop.Core.Data.Orders;
using ReactShop.Core.DTOs;
using ReactShop.Core.Entities;
using ReactShop.Core.Enums;

namespace ReactShop.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly ISaveCustomer _saveCustomer;
        private readonly IGetCustomer _getCustomer;
        private readonly IGetCustomerAddress _getCustomerAddress;
        private readonly IDeleteCustomerAddress _deleteCustomerAddress;
        private readonly IGetOrders _getOrders;
        private readonly ISaveCustomerAddress _saveCustomerAddress;
        private readonly IDeletePaymentOption _deletePaymentOption;
        private readonly ISavePaymentOption _savePaymentOption;

        public UserController()
        {
            _saveCustomer = AutoFacHelper.Resolve<ISaveCustomer>();
            _getCustomer = AutoFacHelper.Resolve<IGetCustomer>();
            _getCustomerAddress = AutoFacHelper.Resolve<IGetCustomerAddress>();
            _deleteCustomerAddress = AutoFacHelper.Resolve<IDeleteCustomerAddress>();
            _getOrders = AutoFacHelper.Resolve<IGetOrders>();
            _saveCustomerAddress = AutoFacHelper.Resolve<ISaveCustomerAddress>();
            _deletePaymentOption = AutoFacHelper.Resolve<IDeletePaymentOption>();
            _savePaymentOption = AutoFacHelper.Resolve<ISavePaymentOption>();
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

        public ActionResult SaveAccount(CustomerDTO accountDetails)
        {
            var customer = _getCustomer.GetById(Identity.LoggedInUserId);
            customer.Title = accountDetails.Title;
            customer.ForeName = accountDetails.ForeName;
            customer.Surname = accountDetails.Surname;
            customer.Email = accountDetails.Email;
            customer.Telephone = accountDetails.Telephone;
            
            _saveCustomer.SaveAccountDetails(CustomerDTO.FromCustomer(customer));
            return RedirectToAction("Index", "Home");
        }

        public ActionResult RemoveAddress(int addressid)
        {
            var address = CustomerAddress.FromDto(_getCustomerAddress.GetCustomerAddressById(addressid));
            var customerId = address.CustomerId;
            _deleteCustomerAddress.Delete(address);

            var customerDTO = CustomerDTO.FromCustomer(_getCustomer.GetById(customerId));
            return View("ManageAccount", customerDTO);
        }

        public ActionResult AddAddress()
        {
            var newAddress = new CustomerAddressDTO()
            {
                CustomerId = Identity.LoggedInUserId
            };

            return View("AddAddress", newAddress);
        }

        public ActionResult EditAddress(int addressId)
        {
            var address = _getCustomerAddress.GetCustomerAddressById(addressId);

            return View("EditAddress", address);
        }

        public ActionResult SaveAddress(CustomerAddressDTO address)
        {
            if (!ValidateAddress(address))
            {
                if (address.Id > 0)
                {
                    return View("EditAddress", address);
                }
                else
                {
                    return View("AddAddress", address);
                }
            }
            _saveCustomerAddress.Save(address);
            var customer = CustomerDTO.FromCustomer(_getCustomer.GetById(address.CustomerId));
            return View("ManageAccount", customer);
        }

        private bool ValidateAddress(CustomerAddressDTO addressDto)
        {
            if (string.IsNullOrEmpty(addressDto.Address1) ||
                string.IsNullOrEmpty(addressDto.Postcode))
            {
                return false;
            }
            return true;
        }

        public ActionResult ManageOrders()
        {
            var orders = _getOrders.GetByCustomer(Identity.LoggedInUserId);
            return View(orders);
        }

        public ActionResult RemovePaymentOption(int paymentOptionId)
        {
            _deletePaymentOption.Delete(paymentOptionId);
            return RedirectToAction("ManageAccount");
        }

        public ActionResult AddPaymentOption()
        {
            var newPaymentOption = new PaymentOptionDTO()
            {
                CustomerId = Identity.LoggedInUserId,
                PaymentType = PaymentTypeEnum.Paypal,
                Status = PaymentOptionStatusEnum.Active
            };

            return View("AddPaymentOption", newPaymentOption);
        }

        public ActionResult SavePaymentOption(PaymentOptionDTO paymentOption)
        {
            if (!ValidatePaymentOption(paymentOption))
            {
                return View("AddPaymentOption", paymentOption);
            }
            _savePaymentOption.Save(paymentOption);
            var customer = CustomerDTO.FromCustomer(_getCustomer.GetById(paymentOption.CustomerId));
            return View("ManageAccount", customer);
        }

        private bool ValidatePaymentOption(PaymentOptionDTO paymentOptionDto)
        {
            if (string.IsNullOrEmpty(paymentOptionDto.PaypalEmail))
            {
                return false;
            }
            return true;
        }
    }
}