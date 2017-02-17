using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReactShop.Core.DTOs
{
    public class CheckoutSummaryDTO
    {
        public string OrderNumber { get; set; }
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Total { get; set; }
        public int DeliveryUpToNWorkingDays { get; set; }

        public string DeliveryUpTo
        {
            get
            {
                return string.Format("{0} {1}", DeliveryUpToNWorkingDays, 
                    DeliveryUpToNWorkingDays == 1 ? " working day" : " working days");
            }
        }

        public CustomerDTO CustomerInfo { get; set; }
        public IEnumerable<OrderItemDTO> OrderItems { get; set; }
    }
}
