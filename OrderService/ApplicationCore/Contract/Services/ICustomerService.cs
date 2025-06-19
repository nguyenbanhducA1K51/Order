using Order.Models;

namespace Order.Contract.Services;

public interface ICustomerService
{
    Task<List<AddressModal>> GetAddressesByCustomerId(int customerId);
    Task<AddressModal>SaveCustomerAddress(AddressModal address,int customerId);
    
    Task<List<CustomerDetailModal>> GetAllCustomersAsync();
    Task<CustomerDetailModal> CreateCustomerAsync(CustomerBaseModal customerBaseModal);
    Task<CustomerDetailModal> GetCustomerDetailAsync(int customerId);
}