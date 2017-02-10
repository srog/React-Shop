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
                    "10 Million Member CodeProject T-Shirt|3399",
                    "Women's T-Shirt|3399",
                    "CodeProject.com Body Suit|1399",
                    "CodeProject Mug Mugs|1099",
                    "RootAdmin Mug|1099",
                    "Drinking Glass|1099",
                    "Stein|1399",
                    "Mousepad|1099",
                    "Square Sticker|299",
                };

                var index = 1;
                foreach (var p in products)
                {
                    var description = p.Split('|')[0];
                    var price = decimal.Parse(p.Split('|')[1]) / 100M;

                    var product =
                        db.Product.Add(new Product
                        {
                            SKU = Guid.NewGuid().ToString(),
                            SmallImagePath = string.Format("Images/Products/small_{0}.jpg", index),
                            LargeImagePath = string.Format("Images/Products/large_{0}.jpg", index),
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
