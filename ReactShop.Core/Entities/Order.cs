using System;
using System.ComponentModel.DataAnnotations.Schema;
using ReactShop.Core.Enums;

namespace ReactShop.Core.Entities
{
    [Table("Order")]
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime DatePlaced { get; set; }
        public int CustomerId { get; set; }
        public OrderStatusEnum Status { get; set; }
        public decimal TotalPrice { get; set; }
        public int DeliveryAddressId { get; set; }
        
    }
}