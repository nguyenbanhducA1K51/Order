using ApplicationCore.Entities;
using Microsoft.Extensions.Logging;
using Order.Contract.Repositories;
using Order.Contract.Services;
using Order.Models;

namespace Infrastructure.Services;

public class CustomerService: ICustomerService
{
    private readonly ITransactionService _transactionService;
    private readonly ICustomerRepository _customerRepository;
    private readonly ILogger<CustomerService> _logger;
    private readonly IAddressRepository _addressRepository;
    private readonly IUserAddressRepository _userAddressRepository;
    public CustomerService(ICustomerRepository customerRepository, 
        IAddressRepository addressRepository,
        IUserAddressRepository userAddressRepository, 
        ITransactionService transactionService,
        ILogger<CustomerService> logger)
    {
        _transactionService=transactionService;
        _customerRepository = customerRepository;
        _logger = logger;
        _addressRepository = addressRepository;
        _userAddressRepository = userAddressRepository;
    }
    public async Task<List<AddressModal>> GetAddressesByCustomerId(int customerId)
    {
        var customer = await _customerRepository.GetById(customerId);
        var addresses = new List<AddressModal>();
        

        // if (customer?.UserAddresses != null)
        // {
        //     foreach (var userAddress in customer.UserAddresses)
        //     {
        //         if (userAddress.Address != null)
        //         {
        //             addresses.Add(new AddressModal
        //             {
        //                 Street1 = userAddress.Address.Street1,
        //                 Street2 = userAddress.Address.Street2,
        //                 City = userAddress.Address.City,
        //                 State = userAddress.Address.State,
        //                 ZipCode = userAddress.Address.ZipCode,
        //                 Country = userAddress.Address.Country,
        //                 IsDefaultAddress = userAddress.IsDefaultAddress // optional
        //             });
        //         }
        //     }
        // }

        return addresses;
        
    }

    public async Task<AddressModal> SaveCustomerAddress(AddressModal address, int customerId)

    {
        return await _transactionService.ExecuteInTransactionAsync(async () =>
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
            if (savedAddress.Id <= 0)
            {
                throw new Exception("Address ID was not generated properly");
            }
            var verifyAddress = await _addressRepository.GetByIdAsync(savedAddress.Id);
            if (verifyAddress == null)
            {
                throw new Exception($"Address with ID {savedAddress.Id} not found in database");
            }
            
            Console.WriteLine($"Verify customer id ID: {customerId}");
            var userAddressEntity = new User_Address
            {
                Customer_Id = customerId,
                Address_Id = savedAddress.Id,
                IsDefaultAddress = address.IsDefaultAddress
            };

            await _userAddressRepository.SaveUserAddress(userAddressEntity);

            
            return address;

        });

    }

    public async Task<CustomerDetailModal> GetCustomerDetailAsync(int customerId)
    {
        var customer = await _customerRepository.GetByIdAsync(customerId);
        var addressDetailList=new List<AddressModal>();
        foreach (var address in customer.UserAddresses.ToList())
        {
            addressDetailList.Add(new AddressModal()
            {
                Street1 = address.Address.Street1,
                Street2 = address.Address.Street2,
                City = address.Address.City,
                State = address.Address.State,
                ZipCode = address.Address.ZipCode,
                Country = address.Address.Country,
                IsDefaultAddress = address.IsDefaultAddress
            });
        }

        return new CustomerDetailModal()
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Gender = customer.Gender,
            Phone = customer.Phone,
            Profile_PIC = customer.Profile_PIC,
            Addresses = addressDetailList,


        };
    }
    public async Task<List<CustomerDetailModal>> GetAllCustomersAsync()
    {
     
        // throw new NotImplementedException();
        var customers =await _customerRepository.GetAll();
        var customerModals = new List<CustomerDetailModal>();
        foreach (var customer in customers)
        {
            customerModals.Add(new CustomerDetailModal()
                {
                    Id = customer.Id,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Gender = customer.Gender,
                    Phone = customer.Phone,
                    Profile_PIC = customer.Profile_PIC,
                }
            );
        }
        return customerModals;
    }

    public async Task<CustomerDetailModal> CreateCustomerAsync(CustomerBaseModal customerBaseModal)
    {
        var customerEntity = new Customer()
        {
            FirstName = customerBaseModal.FirstName,
            LastName = customerBaseModal.LastName,
            Gender = customerBaseModal.Gender,
            Phone = customerBaseModal.Phone,
            Profile_PIC = customerBaseModal.Profile_PIC,

        };
        var customerDetail = new CustomerDetailModal()
        {
            Id = customerEntity.Id,
            FirstName = customerEntity.FirstName,
            LastName = customerEntity.LastName,
            Gender = customerEntity.Gender,
            Phone = customerEntity.Phone,
            Profile_PIC = customerEntity.Profile_PIC,

        };
        await _customerRepository.CreateCustomerAsync(customerEntity);
        return customerDetail;
    }
}