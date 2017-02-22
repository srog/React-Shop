using ReactShop.Core.Entities;

namespace ReactShop.Core.Data.Customers
{
    public class DeleteCustomerAddress : IDeleteCustomerAddress
    {
        public void Delete(CustomerAddress address)
        {
            using (var db = new Context())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    db.CustomerAddress.Attach(address);
                    db.CustomerAddress.Remove(address);
                    db.SaveChanges();
                    transaction.Commit();
                }
            }
        }
    }
}