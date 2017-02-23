using System.ComponentModel.DataAnnotations.Schema;
using ReactShop.Core.DTOs;
using ReactShop.Core.Enums;

namespace ReactShop.Core.Entities
{
    [Table("PaymentOption")]
    public class PaymentOption
    {
        public PaymentOption()
        {
        }
        public PaymentOption(int id, int customerId, PaymentTypeEnum paymentType, string paypalEmail, PaymentOptionStatusEnum status)
        {
            Id = id;
            CustomerId = customerId;
            PaymentType = paymentType;
            PaypalEmail = paypalEmail;
            Status = status;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public PaymentTypeEnum PaymentType { get; set; }
        public string PaypalEmail { get; set; }
        public PaymentOptionStatusEnum Status { get; set; }

        public static PaymentOption FromDto(PaymentOptionDTO dto)
        {
            return new PaymentOption
            {
                CustomerId = dto.CustomerId,
                PaymentType = dto.PaymentType,
                PaypalEmail = dto.PaypalEmail,
                Status = dto.Status
            };
        }
    }
}