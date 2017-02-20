using System.ComponentModel.DataAnnotations.Schema;
using ReactShop.Core.Enums;

namespace ReactShop.Core.Entities
{
    [Table("OrderItem")]
    public class OrderItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int OrderId { get; set; }
        public OrderStatusEnum Status { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
    }
}