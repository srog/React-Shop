using System.Collections.Generic;
using System.Linq;
using ReactShop.Core.Entities;

namespace ReactShop.Core.Data.Categories
{
    public class GetCategory : IGetCategory
    {
        public IEnumerable<Category> GetAll()
        {
            using (var db = new Context())
            {
                return db.Category.ToList();
            }
        }

        public Category GetById(int id)
        {
            using (var db = new Context())
            {
                return db.Category.FirstOrDefault(c => c.Id == id);
            }
        }
    }
}