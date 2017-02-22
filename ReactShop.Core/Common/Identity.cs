using System.Linq;
using ReactShop.Core.Data.Customers;
using ReactShop.Core.DTOs;
using ReactShop.Core.Entities;
using ReactShop.Core.Enums;

namespace ReactShop.Core.Common
{
    public static class Identity
    {
        private static readonly IGetCustomer _getCustomer = AutoFacHelper.Resolve<IGetCustomer>();
        private static readonly IConfigManager _configManager = AutoFacHelper.Resolve<IConfigManager>();
        
        private static Customer currentCustomer = 
            IsTestMode() ? Customer.FromDto(_getCustomer.GetAll().FirstOrDefault()) : null;

        public static int LoggedInUserId => currentCustomer?.Id ?? 0;

        public static bool IsTestMode() => _configManager.GetValue("TestMode") == "true";
        public static bool IsAdminMode() => currentCustomer?.IsAdmin ?? false;

        public static string IdentityName()
        {
            return currentCustomer == null ? "" : CustomerDTO.FromCustomer(currentCustomer).DisplayName;
        }

        public static bool IsLoggedIn()
        {
            return currentCustomer != null;
        }
        
        public static bool Login(string username, string password)
        {
            currentCustomer = _getCustomer.GetByUsernameAndPassword(username, password);
            if (!IsLoggedIn() || currentCustomer.Status != CustomerStatusEnum.Active)
            {
                return false;
            }
            return true;
        }

        public static void Logout()
        {
            currentCustomer = null;
        }
    }
}
