using System;
using ReactShop.Core.Entities;

namespace ReactShop.Core
{
    public class InitializeDB : IInitializeDB
    {
        public Context Init()
        {
            var db = new Context();

            if (!db.Database.Exists())
            {
                db.Database.CreateIfNotExists();

                var products = new string[]
                {
                    "Sheep|sheep1|1499",
                    "Guitar Birthday Card|guitarcard|299",
                    "Alpaca|alpaca1|999",
                    "Sheep|sheep2|1499",
                    "Guitar Birthday Card|guitarcard|299",
                    "Alpaca|alpaca1|999",
                    "Sheep|sheep1|1499",
                    "Guitar Birthday Card|guitarcard|299",
                    "Alpaca|alpaca1|999"

                };

                var index = 1;
                foreach (var p in products)
                {
                    var description = p.Split('|')[0];
                    var filename = p.Split('|')[1];
                    var price = decimal.Parse(p.Split('|')[2]) / 100M;

                    var product =
                        db.Product.Add(new Product
                        {
                            SKU = Guid.NewGuid().ToString(),
                            SmallImagePath = string.Format("Images/Products/{0}.jpg", filename),
                            LargeImagePath = string.Format("Images/Products/{0}.jpg", filename),
                            Description = description,
                            Price = price
                        });

                    var cartItem =
                        db.CartItem.Add(new CartItem
                        {
                            Product = product,
                            Quantity = 1
                        });

                    index++;
                }

                db.Customer.Add(new Customer
                {
                    Id = 1,
                    Title = "Mr",
                    ForeName = "John",
                    Surname = "Doe",
                    Telephone = "(11) 555-12345",
                    Email = "johndoe@email.com",
                    Address1 = "503-250 Ferrand Drive",
                    Address2 = "Toronto Ontario",
                    Address3 = "Canada",
                    Postcode = "M3C 3G8"
                });

                db.SaveChanges();

            }
            return db;
        }
    
    }
}
