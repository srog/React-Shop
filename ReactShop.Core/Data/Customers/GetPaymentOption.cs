using System.Collections.Generic;
using System.Linq;
using ReactShop.Core.DTOs;
using ReactShop.Core.Entities;

namespace ReactShop.Core.Data.Customers
{
    public class GetPaymentOption : IGetPaymentOption
    {
        public IEnumerable<PaymentOption> GetPaymentOptionsForCustomer(int customerId)
        {
            using (var db = new Context())
            {
                return db.PaymentOption.Where(po => po.CustomerId == customerId).ToList();
            }
        }

        public PaymentOptionDTO GetPaymentOptionById(int id)
        {
            using (var db = new Context())
            {
                var paymentOption = db.PaymentOption.FirstOrDefault(po => po.Id == id);
                return PaymentOptionDTO.FromPaymentOption(paymentOption);
            }
        }
    }
}