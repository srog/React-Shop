using System.ComponentModel.DataAnnotations.Schema;

namespace ReactShop.Core.Entities
{
    [Table("CartItem")]
    public class CartItem
    {
        public CartItem()
        {
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //[ForeignKey("Product")]
        public int ProductId { get; set; }

        //[Required]
        //public virtual Product Product { get; set; }

        public int Quantity { get; set; }
        public int CustomerId { get; set; }

        public CartItem(int id, int productId, int quantity, int customerId)
        {
            Id = id;
            ProductId = productId;
            Quantity = quantity;
            CustomerId = customerId;
        }
    }
}