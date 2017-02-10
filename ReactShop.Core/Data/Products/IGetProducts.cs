using System.Collections.Generic;
using ReactShop.Core.DTOs;

namespace ReactShop.Core.Data.Products
{
    public interface IGetProducts
    {
        IEnumerable<ProductDTO> Get();
        ProductDTO GetBySku(string sku);
    }
}
