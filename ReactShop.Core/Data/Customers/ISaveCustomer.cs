using ReactShop.Core.DTOs;

namespace ReactShop.Core.Data.Customers
{
    public interface ISaveCustomer
    {
        int Save(CustomerDTO customer);
    }
}
