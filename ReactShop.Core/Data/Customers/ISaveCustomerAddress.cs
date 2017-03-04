using ReactShop.Core.DTOs;

namespace ReactShop.Core.Data.Customers
{
    public interface ISaveCustomerAddress
    {
        void Save(CustomerAddressDTO address);
    }
}