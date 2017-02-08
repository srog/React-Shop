using ReactShop.Core.DTOs;

namespace ReactShop.Core
{
    public interface IDiscountManager
    {
        DiscountRuleDTO GetDiscount(decimal subtotal);
    }
}