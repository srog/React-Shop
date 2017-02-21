using ReactShop.Core.Entities;

namespace ReactShop.Core.DTOs
{
    public class CustomerAddressDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string Postcode { get; set; }
        
        public string DisplayAddress => (Address1 + "," + Address2 + "," + Postcode);
        
        public static CustomerAddressDTO FromCustomerAddress(CustomerAddress customerAddress)
        {
            return new CustomerAddressDTO
            {
                Id = customerAddress.Id,
                CustomerId = customerAddress.CustomerId,
                Address1 = customerAddress.Address1,
                Address2 = customerAddress.Address2,
                Address3 = customerAddress.Address3,
                Address4 = customerAddress.Address4,
                Postcode = customerAddress.Postcode
            };
        }

        public static explicit operator CustomerAddressDTO(CustomerAddress customerAddress)
        {
            return FromCustomerAddress(customerAddress);
        }
    }
}
