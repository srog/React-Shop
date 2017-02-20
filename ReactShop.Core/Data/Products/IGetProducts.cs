using System.Collections.Generic;
using ReactShop.Core.DTOs;

namespace ReactShop.Core.Data.Products
{
    public interface IGetProducts
    {
        List<ProductDTO> GetAll();
        ProductDTO GetBySku(string sku);
        ProductDTO GetById(int id);
    }
}
