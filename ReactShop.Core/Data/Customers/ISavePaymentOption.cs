using ReactShop.Core.DTOs;

namespace ReactShop.Core.Data.Customers
{
    public interface ISavePaymentOption
    {
        void Save(PaymentOptionDTO paymentOption);
    }
}