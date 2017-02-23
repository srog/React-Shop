using ReactShop.Core.DTOs;

namespace ReactShop.Core
{
    public interface ICheckoutManager
    {
        CheckoutSummaryDTO GetCheckoutSummary(int newOrderId);
        CheckoutConfirmDetailsDTO GetCheckoutConfirmDetails();
    }
}