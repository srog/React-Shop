using System.Data.Entity.Migrations;
using ReactShop.Core.DTOs;
using ReactShop.Core.Entities;

namespace ReactShop.Core.Data.Customers
{
    public class SaveCustomer : ISaveCustomer
    {
        public int Save(CustomerDTO customerDto)
        {
            var result = 0;
            using (var db = new Context())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    var newCustomer = db.Customer.Add(Customer.FromDto(customerDto));

                    db.SaveChanges();
                    transaction.Commit();

                    result = newCustomer.Id;
                }

            }
            return result;
        }

        public void SaveAccountDetails(CustomerDTO customerDto)
        {
            using (var db = new Context())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    var customer = Customer.FromDto(customerDto);
                    db.Customer.AddOrUpdate(customer);

                    db.SaveChanges();
                    transaction.Commit();
                }
            }
        }
    }
}