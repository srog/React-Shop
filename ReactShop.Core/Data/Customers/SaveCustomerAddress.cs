using System.Data.Entity.Migrations;
using ReactShop.Core.DTOs;
using ReactShop.Core.Entities;

namespace ReactShop.Core.Data.Customers
{
    public class SaveCustomerAddress : ISaveCustomerAddress
    {
        public void Save(CustomerAddressDTO address)
        {
            using (var db = new Context())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    var addressToSave = CustomerAddress.FromDto(address);
                    //db.CustomerAddress.Attach(addressToSave);
                    db.CustomerAddress.AddOrUpdate(addressToSave);

                    db.SaveChanges();
                    transaction.Commit();
                }

            }
        }
    }
}