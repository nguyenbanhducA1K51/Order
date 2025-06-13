using ApplicationCore.Entities;
using Microsoft.Extensions.Logging;
using Order.Contract.Repositories;
using Order.Contract.Services;
using Order.Models;

namespace Infrastructure.Services;

public class CustomerService: ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<CustomerService> _logger;
    private readonly IAddressRepository _addressRepository;
    private readonly IUserAddressRepository _userAddressRepository;
    public CustomerService(ICustomerRepository customerRepository, IAddressRepository addressRepository, IUserAddressRepository userAddressRepository,  ILogger<CustomerService> logger)
    {
        _customerRepository = customerRepository;
        _logger = logger;
        _addressRepository = addressRepository;
        _userAddressRepository = userAddressRepository;
    }
    public async Task<List<AddressModal>> GetAddressesByCustomerId(int customerId)
    {
        var customer = await _customerRepository.GetById(customerId);
        var adddresses=new List<AddressModal>();
        foreach (var address in customer.UserAddresses)
        {
            adddresses.Add( new AddressModal()
            {
                Street1 = address.Address.Street1,
                Street2 = address.Address.Street2,
                City = address.Address.City,
                State = address.Address.State,
                ZipCode = address.Address.ZipCode,
                Country = address.Address.Country,
                
            });
        }
        return adddresses;
        
    }

    public async Task<AddressModal> SaveCustomerAddress(AddressModal address, int customerId)

    {
        var customer = await _customerRepository.GetById(customerId);
        if (customer == null)
        {
            throw new Exception("Customer not found");
        }

        var addressEntity = new Address
        {
            Street1 = address.Street1,
            Street2 = address.Street2,
            City = address.City,
            State = address.State,
            ZipCode = address.ZipCode,
            Country = address.Country,
        };

        var savedAddress = await _addressRepository.SaveAddressAsync(addressEntity);

        var userAddressEntity = new User_Address
        {
            Customer_Id = customerId,
            Address_Id = savedAddress.Id,
            IsDefaultAddress = address.isDefaultAddress
        };

        await _userAddressRepository.SaveUserAddress(userAddressEntity);

        return address;
    }
}