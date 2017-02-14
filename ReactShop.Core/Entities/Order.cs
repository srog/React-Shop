using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactShop.Core.Entities
{
    [Table("Order")]
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime DatePlaced { get; set; }
        public int CustomerId { get; set; }
        public int Status { get; set; }
        public IEnumerable<int> Products { get; set; }
        public decimal TotalPrice { get; set; }
        
    }
}