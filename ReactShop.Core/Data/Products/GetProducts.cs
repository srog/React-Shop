using System.Collections.Generic;
using System.Linq;
using ReactShop.Core.DTOs;

namespace ReactShop.Core.Data.Products
{
    public class GetProducts : IGetProducts
    {
        public IEnumerable<ProductDTO> Get()
        {
            using (var db = new Context())
            {
                return db.Product
                    .Select(i =>
                        new ProductDTO
                        {
                            Id = i.Id,
                            SKU = i.SKU,
                            SmallImagePath = i.SmallImagePath,
                            Description = i.Description,
                            Price = i.Price
                        }).ToList();
            }
        }

        public ProductDTO GetBySku(string sku)
        {
            var product = Get().First(p => p.SKU == sku);
            return product;
        }
    }
}