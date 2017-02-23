using ReactShop.Core.Entities;
using ReactShop.Core.Enums;

namespace ReactShop.Core.DTOs
{
    public class PaymentOptionDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public PaymentTypeEnum PaymentType { get; set; }
        public string PaypalEmail { get; set; }
        public PaymentOptionStatusEnum Status { get; set; }

        public static PaymentOptionDTO FromPaymentOption(PaymentOption paymentOption)
        {
            return new PaymentOptionDTO
            {
                Id = paymentOption.Id,
                CustomerId = paymentOption.CustomerId,
                PaymentType = paymentOption.PaymentType,
                PaypalEmail = paymentOption.PaypalEmail,
                Status = paymentOption.Status
            };
        }

        public static explicit operator PaymentOptionDTO(PaymentOption paymentOption)
        {
            return FromPaymentOption(paymentOption);
        }
    }
}
