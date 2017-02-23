using System.Collections.Generic;
using ReactShop.Core.DTOs;
using ReactShop.Core.Entities;

namespace ReactShop.Core.Data.Customers
{
    public interface IGetPaymentOption
    {
        IEnumerable<PaymentOption> GetPaymentOptionsForCustomer(int customerId);
        PaymentOptionDTO GetPaymentOptionById(int id);
    }
}
