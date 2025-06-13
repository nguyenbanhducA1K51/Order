using Microsoft.AspNetCore.Mvc;
using Order.Contract.Services;
using Order.Models;

namespace API.Controller;
[ApiController]
[Route("api/[controller]")]
public class PaymentController:ControllerBase
{
    private readonly IPaymentMethodService _paymentMethodService;
    private readonly IPaymentTypeService _paymentTypeService;

    public PaymentController(IPaymentMethodService paymentMethodService, IPaymentTypeService paymentTypeService)
    {
        _paymentMethodService = paymentMethodService;
        _paymentTypeService = paymentTypeService;
    }

    [HttpGet("GetPaymentByCustomerId/{customerId}")]
    public async Task<ActionResult<PaymentMethodModal> >GetPaymentByCustomerId(int customerId)
    {
        return NoContent();
    }

    [HttpPost("SavePayment")]
    public async Task<IActionResult> SavePayment(PaymentMethodModal payment)
    {
        return NoContent();
    }

    [HttpDelete("DeletePayment")]
    public async Task<IActionResult> DeletePayment(int id)
    {
        return NoContent();
    }

    [HttpPut("UpdatePayment")]
    public async Task<IActionResult> UpdatePayment(PaymentMethodModal payment)
    {
        return NoContent();
    }
}