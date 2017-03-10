using ReactShop.Core.Entities;

namespace ReactShop.Core.Data.Customers
{
    public interface IDeletePaymentOption
    {
        void Delete(PaymentOption paymentOption);
        void Delete(int id);
    }
}