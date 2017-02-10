using ReactShop.Core.DTOs;
using ReactShop.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace ReactShop.Core
{
    public class CheckoutManager : ICheckoutManager
    {
        private string serverFilePath;

        public CheckoutManager()
        {
        }

        public CheckoutManager(string serverFilePath)
        {
            this.serverFilePath = serverFilePath;
        }

        public ProductDTO GetProduct(string SKU)
        {
            var product = GetProducts().First();
            return product;
        }

        public CustomerDTO GetCustomer(int id)
        {
            var customer = GetCustomers().FirstOrDefault(c => c.Id == id);
            return customer;
        }

        public CheckoutSummaryDTO GetCheckoutSummary()
        {
            var cartItems = GetCartItems();
            var subtotal = cartItems.Sum(i => i.Subtotal);
            var discountRule = DiscountManager.Instance.GetDiscount(subtotal);
            var discountValue = discountRule.CalculatedDiscount;
            var total = subtotal - discountValue;

            return new CheckoutSummaryDTO
            {
                OrderNumber = "123456789",
                DeliveryUpToNWorkingDays = 4,
                Total = total,
                CustomerInfo = GetCustomer(1),
                CartItems = cartItems
            };
        }

        public CartDTO GetCart()
        {
            return GetCart(GetCartItems());
        }

        public CartDTO GetCart(List<CartItemDTO> cartItems)
        {
            var subtotal = cartItems.Sum(i => i.Subtotal);
            var discountRule = DiscountManager.Instance.GetDiscount(subtotal);
            var discountValue = discountRule.CalculatedDiscount;
            var total = subtotal - discountValue;

            return new CartDTO
            {
                Subtotal = subtotal,
                DiscountRate = discountRule.Rate * 100M,
                DiscountValue = discountValue,
                Total = total,
                CartItems = cartItems,
                CustomerId = 1
            };
        }

        public void SaveCart(CartItemDTO newOrEditItem)
        {
            try
            {
                if (newOrEditItem.Quantity < 0)
                    newOrEditItem.Quantity = 0;

                using (var db = new Context())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        var product = db.Product.Where(p => p.SKU == newOrEditItem.SKU).Single();

                        var cartItem =
                            (from ci in db.CartItem
                             join p in db.Product on ci.ProductId equals p.Id
                             where p.SKU == newOrEditItem.SKU
                             select ci)
                            .SingleOrDefault();

                        if (cartItem != null)
                        {
                            if (newOrEditItem.Quantity == 0)
                                db.CartItem.Remove(cartItem);
                            else
                            {
                                cartItem.Quantity = newOrEditItem.Quantity;
                                cartItem.Product = product;
                            }
                        }
                        else
                        {
                            db.CartItem.Add(new CartItem
                            {
                                Product = product,
                                Quantity = newOrEditItem.Quantity
                            });
                        }

                        db.SaveChanges();
                        transaction.Commit();
                    }
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage);
                    }
                }
            }
        }

        public List<CustomerDTO> GetCustomers()
        {
            using (var db = new Context())
            {
                return db.Customer
                    .Select(i =>
                    new CustomerDTO
                    {
                        Id = i.Id,
                        Title = i.Title,
                        ForeName = i.ForeName,
                        Surname = i.Surname,
                        Address1 = i.Address1,
                        Address2 = i.Address2,
                        Address3 = i.Address3,
                        Postcode = i.Postcode
                    }).ToList();
            }
        }

        public List<ProductDTO> GetProducts()
        {
            using (var db = new Context())
            {
                return db.Product
                    .Select(i =>
                    new ProductDTO
                    {
                        Id = i.Id,
                        SKU = i.SKU,
                        SmallImagePath = i.SmallImagePath,
                        Description = i.Description,
                        Price = i.Price
                    }).ToList();
            }
        }

        private List<CartItemDTO> GetCartItems()
        {
            using (var db = new Context())
            {
                return 
                    (from ci in db.CartItem
                    from p in db.Product.Where(p => p.Id == ci.ProductId)
                    select new { ci, p })
                    .Select(i =>
                    new CartItemDTO
                    {
                        Id =  i.ci.Id,
                        SKU = i.p.SKU,
                        SmallImagePath = i.p.SmallImagePath,
                        Description = i.p.Description,
                        Price = i.p.Price,
                        Quantity = i.ci.Quantity
                    }).ToList();
            }
        }

        public Context InitializeDB()
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

        public int CreateOrder(CartDTO cart)
        {
            var result = 0;
            using (var db = new Context())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    db.Order.Add(new Order
                    {
                        CustomerId = cart.CustomerId,
                        DatePlaced = DateTime.Now,
                        Products = cart.CartItems.Select(ci => ci.Id),
                        TotalPrice = cart.Total
                    });

                    result = db.SaveChanges();
                    transaction.Commit();
                }
            }
            return result;
        }
    }
}
