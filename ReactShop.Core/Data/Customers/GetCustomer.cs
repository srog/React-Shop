using System.Collections.Generic;
using System.Linq;
using ReactShop.Core.DTOs;
using ReactShop.Core.Entities;

namespace ReactShop.Core.Data.Customers
{
    public class GetCustomer : IGetCustomer
    {
        public IEnumerable<CustomerDTO> GetAll()
        {
            using (var db = new Context())
            {
                return db.Customer
                    .Select(i =>
                        new CustomerDTO
                        {
                            Id = i.Id,
                            Username = i.Username,
                            Password = i.Password,
                            Title = i.Title,
                            ForeName = i.ForeName,
                            Surname = i.Surname,
                            Email = i.Email,
                            Telephone = i.Telephone,
                            Status = i.Status,
                            IsAdmin = i.IsAdmin
                        }).ToList();
            }
        }

        public Customer GetById(int id)
        {
            using (var db = new Context())
            {
                return db.Customer.FirstOrDefault(c => c.Id == id);
            }
        }

        public Customer GetByUsernameAndPassword(string username, string password)
        {
            using (var db = new Context())
            {
                return db.Customer.FirstOrDefault(c => c.Username == username && c.Password == password);
            }
        }

        public bool UsernameExists(string username)
        {
            using (var db = new Context())
            {
                return db.Customer.Any(c => c.Username == username);
            }
        }
    }
}