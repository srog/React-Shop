using ReactShop.Core.DTOs;

namespace ReactShop.Core
{
    public interface ICheckoutManager
    {
        void SaveCart(CartItemDTO modifiedItem);
        CheckoutSummaryDTO GetCheckoutSummary();

    }
}