using System.Collections.Generic;
using ReactShop.Core.Entities;

namespace ReactShop.Core.Data.Categories
{
    public interface IGetCategory
    {
        IEnumerable<Category> GetAll();
        Category GetById(int id);
    }
}
