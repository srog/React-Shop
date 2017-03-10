using ReactShop.Core.Entities;

namespace ReactShop.Core.Data.Customers
{
    public class DeletePaymentOption : IDeletePaymentOption
    {
        public void Delete(PaymentOption paymentOption)
        {
            using (var db = new Context())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    db.PaymentOption.Attach(paymentOption);
                    db.PaymentOption.Remove(paymentOption);
                    db.SaveChanges();
                    transaction.Commit();
                }
            }
        }

        public void Delete(int id)
        {
            using (var db = new Context())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    var po = new PaymentOption {Id = id};
                    db.PaymentOption.Attach(po);
                    db.PaymentOption.Remove(po);
                    db.SaveChanges();
                    transaction.Commit();
                }
            }
        }
    }
}