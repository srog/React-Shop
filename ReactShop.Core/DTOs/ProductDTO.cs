using ReactShop.Core.Entities;

namespace ReactShop.Core.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public string SmallImagePath { get; set; }
        public string LargeImagePath { get; set; }
        public decimal Price { get; set; }


        public static explicit operator Product(ProductDTO dto)
        {
            return dto.ToProduct();
        }

        public Product ToProduct()
        {
            return new Product(Id, SKU, Description, SmallImagePath, LargeImagePath, Price);
        }
    }
}