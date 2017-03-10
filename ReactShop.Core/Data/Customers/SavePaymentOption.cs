using System.Data.Entity.Migrations;
using ReactShop.Core.DTOs;
using ReactShop.Core.Entities;

namespace ReactShop.Core.Data.Customers
{
    public class SavePaymentOption : ISavePaymentOption
    {
        public void Save(PaymentOptionDTO paymentOption)
        {
            using (var db = new Context())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    var paymentOptionToSave = PaymentOption.FromDto(paymentOption);
                    db.PaymentOption.AddOrUpdate(paymentOptionToSave);

                    db.SaveChanges();
                    transaction.Commit();
                }
            }
        }
    }
}