using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactShop.Core.Data.Cart;

namespace ReactShop.Core.Common
{
    public static class Helpers
    {
         public static int GetCartCount()
        {
            return GetCartItem.GetCartCount();
        }
    }
}
