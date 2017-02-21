using System.ComponentModel.DataAnnotations.Schema;
using ReactShop.Core.DTOs;
using ReactShop.Core.Enums;

namespace ReactShop.Core.Entities
{
    [Table("Customer")]
    public class Customer
    {
        public Customer()
        {
        }
        public Customer(int id, string username, string password, string title, string forename, string surname,
            string telephone, string email, CustomerStatusEnum status)
        {
            Id = id;
            Username = username;
            Password = password;
            Title = title;
            ForeName = forename;
            Surname = surname;
            Telephone = telephone;
            Email = email;
            Status = status;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Title { get; set; }
        public string ForeName { get; set; }
        public string Surname { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public CustomerStatusEnum Status { get; set; }

        public static Customer FromDto(CustomerDTO dto)
        {
            return new Customer(dto.Id, dto.Username, dto.Password, dto.Title, dto.ForeName, dto.Surname, 
                dto.Telephone, dto.Email, dto.Status);
        }
    }
}