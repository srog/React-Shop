using System.Collections.Generic;
using System.Linq;
using ReactShop.Core.DTOs;
using ReactShop.Core.Enums;

namespace ReactShop.Core.Data.Products
{
    public class GetProducts : IGetProducts
    {
        public List<ProductDTO> Get()
        {
            using (var db = new Context())
            {
                return db.Product.Select(p => new ProductDTO
                {
                    Id = p.Id,
                    Description = p.Description,
                    SKU = p.SKU,
                    CategoryId = p.CategoryId,
                    SmallImagePath = p.SmallImagePath,
                    LargeImagePath = p.LargeImagePath,
                    Price = p.Price,
                    Status = p.Status
                }).Where(p => p.Status == ProductStatusEnum.Live).ToList();
            }
        }

        public ProductDTO GetBySku(string sku)
        {
            var product = Get().FirstOrDefault(p => p.SKU == sku);
            return product == null ? null : new ProductDTO
            {
                SKU = product.SKU,
                CategoryId = product.CategoryId,
                Description = product.Description,
                SmallImagePath = product.SmallImagePath,
                LargeImagePath = product.LargeImagePath,
                Id = product.Id,
                Price = product.Price,
                Status = product.Status
            };
        }

        public ProductDTO GetById(int id)
        {
            var product = Get().FirstOrDefault(p => p.Id == id);
            return product == null ? null : new ProductDTO
            {
                SKU = product.SKU,
                CategoryId = product.CategoryId,
                Description = product.Description,
                SmallImagePath = product.SmallImagePath,
                LargeImagePath = product.LargeImagePath,
                Id = product.Id,
                Price = product.Price,
                Status = product.Status
            };
        }
    }
}