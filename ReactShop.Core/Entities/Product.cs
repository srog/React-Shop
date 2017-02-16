using System.ComponentModel.DataAnnotations.Schema;

namespace ReactShop.Core.Entities
{
    [Table("Product")]
    public class Product
    {
        public Product()
        {
            
        }

        public Product(int id, string sku, int categoryId, string description, string smallImagePath, string largeImagePath,
            decimal price)
        {
            Id = id;
            SKU = sku;
            CategoryId = categoryId;
            Description = description;
            SmallImagePath = smallImagePath;
            LargeImagePath = largeImagePath;
            Price = price;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string SKU { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public string SmallImagePath { get; set; }
        public string LargeImagePath { get; set; }
        public decimal Price { get; set; }
    }


}