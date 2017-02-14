using ReactShop.Core.Data.Customers;

namespace ReactShop.Core.Common
{
    public static class Identity
    {
        private static readonly IGetCustomer _getCustomer = AutoFacHelper.Resolve<IGetCustomer>();
        private static int CurrentUserId = 0;

        public static int LoggedInUserId => CurrentUserId;

        public static string IdentityName()
        {
            return CurrentUserId == 0 ? "" :  _getCustomer.GetById(CurrentUserId).DisplayName;
        }

        public static bool IsLoggedIn()
        {
            return CurrentUserId > 0;
        }

        public static void Login()
        {
            CurrentUserId = 1;
        }

        public static void Logout()
        {
            CurrentUserId = 0; 
        }



    }
}
