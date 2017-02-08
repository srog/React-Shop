using System.ComponentModel.DataAnnotations.Schema;

namespace ReactShop.Core.Entities
{
    [Table("Product")]
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }
        public string SmallImagePath { get; set; }
        public string LargeImagePath { get; set; }
        public decimal Price { get; set; }
    }
}