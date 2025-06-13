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
    public async Task<IActionResult>SaveCustomerAddress(AddressModal address, int CustomerId)
    {
        return NoContent();
    }
}