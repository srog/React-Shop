using System.ComponentModel.DataAnnotations.Schema;
using ReactShop.Core.DTOs;

namespace ReactShop.Core.Entities
{
    [Table("CustomerAddress")]
    public class CustomerAddress
    {
        public CustomerAddress()
        {
        }
        public CustomerAddress(int id, int customerId,
            string address1, string address2, string address3, string address4, string postcode)
        {
            Id = id;
            CustomerId = customerId;
            Address1 = address1;
            Address2 = address2;
            Address3 = address3;
            Address4 = address4;
            Postcode = postcode;
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Postcode { get; set; }

        public string DisplayAddress => Address1 + "," + Address2 + "," + Address3 + "," + Address4 + "," + Postcode;

        public static CustomerAddress FromDto(CustomerAddressDTO dto)
        {
            return new CustomerAddress(dto.Id, dto.CustomerId,
                dto.Address1, dto.Address2, dto.Address3, dto.Address4, dto.Postcode);
        }
    }
}