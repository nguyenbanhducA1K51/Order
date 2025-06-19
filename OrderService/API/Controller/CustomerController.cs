using ApplicationCore.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Order.Contract.Repositories;
using Order.Contract.Services;
using Order.Models;

namespace API.Controller;

[ApiController]
[Route("api/[controller]")]
public class CustomerController:ControllerBase
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet("GetCustomerAddressById")]
    public async Task<ActionResult<AddressModal> >GetCustomerAddressById(int id)
    {
        return NoContent();

    }
    
    [HttpPost("SaveCustomerAddress")]
    public async Task<ActionResult<AddressModal>>SaveCustomerAddress(AddressModal address, int CustomerId)
    {
        var addresSave=await _customerService.SaveCustomerAddress(address,CustomerId);
        return Ok(addresSave);
    }

    [HttpGet("GetAllCustomers")]
    public async Task<ActionResult<List<CustomerBaseModal>> >GetAllCustomers()
    {
        var customerModal = await _customerService.GetAllCustomersAsync();
            return Ok(customerModal);
    }
    [HttpGet("GetCustomerById")]
    public async Task<ActionResult<List<CustomerDetailModal>> >GetCustomerById(int id)
    {
        var customerModal = await _customerService.GetCustomerDetailAsync(id);
        return Ok(customerModal);
    }

    [HttpPost("SaveCustomer")]
    public async Task<IActionResult> SaveCustomer(CustomerBaseModal customerBaseModal)
    {
        var customer=await _customerService.CreateCustomerAsync(customerBaseModal);
        return Ok(customer);
    }
}