using ReactShop.Core.Entities;
using System.Data.Entity;

namespace ReactShop.Core
{
    public class Context : DbContext
    {
        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<CustomerAddress> CustomerAddress { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Category> Category { get; set; }
    }
}
