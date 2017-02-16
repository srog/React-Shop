using ReactShop.Core.DTOs;
using ReactShop.Core.Entities;

namespace ReactShop.Core.Data.Products
{
    public class SaveProduct : ISaveProduct
    {
        public int Save(ProductDTO productDto)
        {
            var result = 0;
            using (var db = new Context())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    db.Product.Add(new Product
                    {
                        SKU = productDto.SKU,
                        CategoryId = productDto.CategoryId,
                        Description = productDto.Description,
                        SmallImagePath = productDto.SmallImagePath,
                        LargeImagePath = productDto.LargeImagePath,
                        Price = productDto.Price
                    });

                    result = db.SaveChanges();
                    transaction.Commit();
                }
            }
            return result;
        }
    }
}
