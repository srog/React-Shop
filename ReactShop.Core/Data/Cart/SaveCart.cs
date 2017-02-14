using System.Data.Entity.Migrations;
using ReactShop.Core.Entities;

namespace ReactShop.Core.Data.Cart
{
    public class SaveCart : ISaveCart
    {
        public void Save(CartItem cartItem)
        {
            using (var db = new Context())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    db.CartItem.AddOrUpdate(cartItem);
                    db.SaveChanges();
                    transaction.Commit();
                }
            }
        }

        public void Remove(CartItem cartItem)
        {
            using (var db = new Context())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    db.CartItem.Attach(cartItem);

                    db.CartItem.Remove(cartItem);
                    db.SaveChanges();
                    transaction.Commit();
                }
            }

        }
    }
}