using System;
using System.Linq;
using ReactShop.Core.Entities;
using ReactShop.Core.Enums;

namespace ReactShop.Core
{
    public class InitializeDB : IInitializeDB
    {
        public Context Init()
        {
            var db = new Context();

            if (db.Database.Exists())
            {
                // initial clear out of data
                // remove any cart items, orders
                var rows = from ci in db.CartItem
                           select ci;
                foreach (var row in rows)
                {
                    db.CartItem.Remove(row);
                }
                var orders = from ord in db.Order   
                           select ord;
                foreach (var order in orders)
                {
                    db.Order.Remove(order);
                }
                foreach (var orderItem in db.OrderItem)
                {
                    db.OrderItem.Remove(orderItem);
                }
                db.SaveChanges();
            }
            else
            {
                // create DB if doesn't exist
                // initialise categories, products, customers
                db.Database.CreateIfNotExists();

                db.Category.Add(new Category()
                {
                    Description = "Cards",
                    Id = 1
                });
                db.Category.Add(new Category()
                {
                    Description = "Other",
                    Id = 2
                });

                var products = new string[]
                {
                    "Sheep|sheep1|1499|2",
                    "Guitar Birthday Card|guitarcard|299|1",
                    "Alpaca|alpaca1|999|2",
                    "Sheep|sheep2|1499|2",
                    "Guitar Birthday Card|guitarcard|299|1",
                    "Alpaca|alpaca1|999|2",
                    "Sheep|sheep1|1499|2",
                    "Guitar Birthday Card|guitarcard|299|1",
                    "Alpaca|alpaca1|999|2",
                    "Sheep|sheep1|1499|2",
                    "Guitar Birthday Card|guitarcard|299|1",
                    "Alpaca|alpaca1|999|2",
                    "Sheep|sheep2|1499|2",
                    "Guitar Birthday Card|guitarcard|299|1",
                    "Alpaca|alpaca1|999|2",
                    "Sheep|sheep1|1499|2",
                    "Guitar Birthday Card|guitarcard|299|1",
                    "Alpaca|alpaca1|999|2",

                    "Sheep|sheep1|1499|2",
                    "Guitar Birthday Card|guitarcard|299|1",
                    "Alpaca|alpaca1|999|2",
                    "Sheep|sheep2|1499|2",
                    "Guitar Birthday Card|guitarcard|299|1",
                    "Alpaca|alpaca1|999|2",
                    "Sheep|sheep1|1499|2",
                    "Guitar Birthday Card|guitarcard|299|1",
                    "Alpaca|alpaca1|999|2",
                    "Sheep|sheep1|1499|2",
                    "Guitar Birthday Card|guitarcard|299|1",
                    "Alpaca|alpaca1|999|2",
                    "Sheep|sheep2|1499|2",
                    "Guitar Birthday Card|guitarcard|299|1",
                    "Alpaca|alpaca1|999|2",
                    "Sheep|sheep1|1499|2",
                    "Guitar Birthday Card|guitarcard|299|1",
                    "Alpaca|alpaca1|999|2"


                };

                var index = 1;
                foreach (var p in products)
                {
                    var productBits = p.Split('|');
                    var description = productBits[0];
                    var filename = productBits[1];
                    var price = decimal.Parse(productBits[2]) / 100M;
                    var category = int.Parse(productBits[3]);

                    var product =
                        db.Product.Add(new Product
                        {
                            SKU = Guid.NewGuid().ToString(),
                            SmallImagePath = string.Format("Images/Products/{0}.jpg", filename),
                            LargeImagePath = string.Format("Images/Products/{0}.jpg", filename),
                            Description = description,
                            CategoryId = category,
                            Price = price,
                            Status = ProductStatusEnum.Live
                        });

                    
                    index++;
                }
                
                db.Customer.Add(new Customer(1, "testuser", "password1", "Mr", "John", "Doe", "(11) 555-12345",
                    "johndoe@email.com",
                    "503-250 Ferrand Drive", "Toronto Ontario", "Canada", "M3C 3G8", CustomerStatusEnum.Active));

                db.SaveChanges();

            }
            return db;
        }
    
    }
}
