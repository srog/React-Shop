using ReactShop.Core.DTOs;

namespace ReactShop.Core.Data.Products
{
    public interface ISaveProduct
    {
        int Save(ProductDTO product);
    }
}