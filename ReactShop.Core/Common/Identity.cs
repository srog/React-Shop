using ReactShop.Core.Data.Customers;
using ReactShop.Core.DTOs;

namespace ReactShop.Core.Common
{
    public static class Identity
    {
        private static readonly IGetCustomer _getCustomer = AutoFacHelper.Resolve<IGetCustomer>();
        private static readonly IConfigManager _configManager = AutoFacHelper.Resolve<IConfigManager>();

        private static int CurrentUserId = _configManager.GetValue("AdminMode") == "true" ? 1: 0;

        public static int LoggedInUserId => CurrentUserId;

        public static string IdentityName()
        {
            return CurrentUserId == 0 ? "" : CustomerDTO.FromCustomer(_getCustomer.GetById(CurrentUserId)).DisplayName;
        }

        public static bool IsLoggedIn()
        {
            return CurrentUserId > 0;
        }

        public static void Login()
        {
            CurrentUserId = 1;
        }

        public static void Login(string customerId)
        {
            CurrentUserId = int.Parse(customerId);
        }

        public static void Logout()
        {
            CurrentUserId = 0; 
        }

    }
}
