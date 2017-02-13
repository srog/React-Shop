using System.Collections.Generic;
using ReactShop.Core.DTOs;

namespace ReactShop.Core.Data.Products
{
    public interface IGetProducts
    {
        List<ProductDTO> Get();
        ProductDTO GetBySku(string sku);
        ProductDTO GetById(int id);
    }
}
