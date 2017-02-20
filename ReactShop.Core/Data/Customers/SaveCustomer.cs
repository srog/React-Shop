﻿using ReactShop.Core.DTOs;
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
                    var newCustomer = db.Customer.Add(new Customer
                    {
                        Username = customerDto.Username,
                        Password = customerDto.Password,
                        Title = customerDto.Title,
                        ForeName = customerDto.ForeName,
                        Surname = customerDto.Surname,
                        Email = customerDto.Email,
                        Telephone = customerDto.Telephone,
                        Address1 = customerDto.Address1,
                        Address2 = customerDto.Address2,
                        Address3 = customerDto.Address3,
                        Postcode = customerDto.Postcode
                    });

                    db.SaveChanges();
                    transaction.Commit();

                    result = newCustomer.Id;
                }

            }
            return result;
        }
    }
}