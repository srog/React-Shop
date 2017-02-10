using ReactShop.Core.Entities;
using System.Data.Entity;

namespace ReactShop.Core
{
    public class Context : DbContext
    {
        public DbSet<CartItem> CartItem { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Customer> Customer { get; set; }
    }
}
