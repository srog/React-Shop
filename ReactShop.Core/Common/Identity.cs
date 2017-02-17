using ReactShop.Core.Data.Customers;
using ReactShop.Core.DTOs;

namespace ReactShop.Core.Common
{
    public static class Identity
    {
        private static readonly IGetCustomer _getCustomer = AutoFacHelper.Resolve<IGetCustomer>();
        private static readonly IConfigManager _configManager = AutoFacHelper.Resolve<IConfigManager>();

        private static int CurrentUserId = IsAdminMode() ? 1: 0;

        public static int LoggedInUserId => CurrentUserId;

        public static bool IsAdminMode() => _configManager.GetValue("AdminMode") == "true";

        public static string IdentityName()
        {
            return CurrentUserId == 0 ? "" : CustomerDTO.FromCustomer(_getCustomer.GetById(CurrentUserId)).DisplayName;
        }

        public static bool IsLoggedIn()
        {
            return CurrentUserId > 0;
        }
        
        public static bool Login(string username, string password)
        {
            var customer = _getCustomer.GetByUsernameAndPassword(username, password);
            CurrentUserId = customer?.Id ?? 0;
            return customer != null;
        }

        public static void Logout()
        {
            CurrentUserId = 0; 
        }

    }
}
