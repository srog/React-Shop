using System;
using System.Linq;
using ReactShop.Core.Common;
using ReactShop.Core.Data.Products;
using ReactShop.Core.Entities;
using ReactShop.Core.Enums;

namespace ReactShop.Core
{
    public class InitializeDB : IInitializeDB
    {
        private readonly IGetProducts _getProduct;

        public InitializeDB()
        {
            _getProduct = AutoFacHelper.Resolve<IGetProducts>();
        }
        public Context Init()
        {
            var db = new Context();
       
            db.Database.CreateIfNotExists();
            
            // clear all data
            // populate with static data
            // if TestMode, populate with dummy orders and customer info
                
            ClearAllData(db);
            AddStaticData(db);

            if (Identity.IsTestMode())
            {
                AddDummyData(db);
            }

            return db;
        }

        private void ClearAllData(Context db)
        {
            foreach (var row in db.CartItem)
            {
                db.CartItem.Remove(row);
            }
            foreach (var row in db.OrderItem)
            {
                db.OrderItem.Remove(row);
            }
            foreach (var row in db.Order)
            {
                db.Order.Remove(row);
            }
            foreach (var row in db.CustomerAddress)
            {
                db.CustomerAddress.Remove(row);
            }
            foreach (var row in db.Customer)
            {
                db.Customer.Remove(row);
            }
            foreach (var row in db.Product)
            {
                db.Product.Remove(row);
            }
            foreach (var row in db.Category)
            {
                db.Category.Remove(row);
            }

            db.SaveChanges();
        }

        private void AddStaticData(Context db)
        {
            var cat1 = new Category()
            {
                Description = "Cards"
            };
            var cat2 = new Category()
            {
                Description = "Other Stuff"
            };

            db.Category.Add(cat1);
            db.SaveChanges();
            db.Category.Add(cat2);
            db.SaveChanges();

            var products = new string[]
             {
                    "Sheep|sheep1|1499",
                    "Guitar Birthday Card|guitarcard|299",
                    "Alpaca|alpaca1|999",
             };

            var index = 1;
            foreach (var p in products)
            {
                var productBits = p.Split('|');
                var description = productBits[0];
                var filename = productBits[1];
                var price = decimal.Parse(productBits[2]) / 100M;
                
                db.Product.Add(new Product
                {
                    SKU = Guid.NewGuid().ToString(),
                    SmallImagePath = string.Format("Images/Products/{0}.jpg", filename),
                    LargeImagePath = string.Format("Images/Products/{0}.jpg", filename),
                    Description = description,
                    CategoryId = cat1.Id,
                    Price = price,
                    Status = ProductStatusEnum.Live
                });

                db.Product.Add(new Product
                {
                    SKU = Guid.NewGuid().ToString(),
                    SmallImagePath = string.Format("Images/Products/{0}.jpg", filename),
                    LargeImagePath = string.Format("Images/Products/{0}.jpg", filename),
                    Description = description,
                    CategoryId = cat2.Id,
                    Price = price,
                    Status = ProductStatusEnum.Live
                });

                index++;
            }

            db.SaveChanges();

            // Default / Admin user

            db.Customer.Add(new Customer(1, "adminuser", "password1", "Mr", "Test", "Admin",
                  "(11) 555-12345", "testadmin@email.com", CustomerStatusEnum.Active, true));

            db.SaveChanges();

            db.CustomerAddress.Add(new CustomerAddress(1, 1, "503-250 Ferrand Drive", "Toronto", "Ontario",
                "Canada", "M3C 3G8"));

            db.SaveChanges();
        }

        private void AddDummyData(Context db)
        {
            var customerCount = 20;

            for (var customerIndex = 1; customerIndex <= customerCount; customerIndex++)
            {
                var custStatus = CustomerStatusEnum.Active;
                if (customerIndex > 15)
                {
                    custStatus = CustomerStatusEnum.Blocked;
                }
                if (customerIndex > 18)
                {
                    custStatus = CustomerStatusEnum.Deleted;
                }

                var customer = new Customer()
                {
                    Title = "Mr",
                    ForeName = "Test" + customerIndex,
                    Surname = "Test" + customerIndex,
                    Username = "user" + customerIndex,
                    Password = "password" + customerIndex,
                    Email = "test" + customerIndex + "@test.com",
                    IsAdmin = true,
                    Status = custStatus,
                    Telephone = "123123" + customerIndex
                };

                db.Customer.Add(customer);
                db.SaveChanges();

                var customerAddress = new CustomerAddress()
                {
                    CustomerId = customer.Id,
                    Address1 = "" + customer.Id + " Test Street",
                    Address2 = "Test Town",
                    Address3 = "Test City",
                    Address4 = "GB",
                    Postcode = "SK1 T" + customerIndex
                };
                db.CustomerAddress.Add(customerAddress);
                db.SaveChanges();

                var altcustomerAddress = new CustomerAddress()
                {
                    CustomerId = customer.Id,
                    Address1 = "" + customer.Id + " Alternative Street",
                    Address2 = "Alternative Town",
                    Address3 = "Alternative City",
                    Address4 = "USA",
                    Postcode = "NYNYNY" + customerIndex
                };
                db.CustomerAddress.Add(altcustomerAddress);
                db.SaveChanges();

                //add orders for this customer

                var ordStatus = OrderStatusEnum.Booked;
                if (customerIndex > 12)
                {
                    ordStatus = OrderStatusEnum.Completed; ;
                }
                if (customerIndex > 15)
                {
                    ordStatus = OrderStatusEnum.Cancelled;
                }
                if (customerIndex > 18)
                {
                    ordStatus = OrderStatusEnum.OnHold;
                }

                var order = new Order
                {
                    CustomerId = customer.Id,
                    Status = ordStatus,
                    DatePlaced = DateTime.Now.AddDays(-100 + customerIndex * 3),
                    DeliveryAddressId = customerAddress.Id,
                    TotalPrice = 50 + customerIndex
                };
                db.Order.Add(order);
                db.SaveChanges();

                var orderItem = new OrderItem()
                {
                    Description = "Order item for cust " + customerIndex,
                    OrderId = order.Id,
                    Price = 50 + customerIndex,
                    ProductId = _getProduct.GetAll().FirstOrDefault().Id,
                    Quantity = 1,
                    Status = ordStatus
                };
                db.OrderItem.Add(orderItem);
                db.SaveChanges();

                if (customerIndex == 5 || customerIndex == 10 || customerIndex == 15)
                {
                    var orderItem2 = new OrderItem
                    {
                        Description = "Another Order item for cust " + customerIndex,
                        OrderId = order.Id,
                        Price = 250 + customerIndex,
                        ProductId = _getProduct.GetAll()[1].Id,
                        Quantity = 2,
                        Status = ordStatus
                    };
                    db.OrderItem.Add(orderItem2);
                    db.SaveChanges();
                }

                if (customerIndex > 15)
                {
                    ordStatus = OrderStatusEnum.Booked;
                    var order2 = new Order
                    {
                        CustomerId = customer.Id,
                        Status = ordStatus,
                        DatePlaced = DateTime.Now.AddDays(-100 + customerIndex*3),
                        DeliveryAddressId = altcustomerAddress.Id,
                        TotalPrice = 50 + customerIndex
                    };
                    db.Order.Add(order2);
                    db.SaveChanges();

                    var orderItemextra = new OrderItem()
                    {
                        Description = "Additional Order item " + customerIndex,
                        OrderId = order2.Id,
                        Price = 50 + customerIndex,
                        ProductId = _getProduct.GetAll()[2].Id,
                        Quantity = 3,
                        Status = ordStatus
                    };
                    db.OrderItem.Add(orderItemextra);
                    db.SaveChanges();

                }
            }

            db.SaveChanges();
        }
    }
}
