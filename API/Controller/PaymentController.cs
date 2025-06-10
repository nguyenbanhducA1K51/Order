using Microsoft.AspNetCore.Mvc;
using Order.Contract.Services;

namespace API.Controller;
[ApiController]
[Route("api/[controller]")]
public class PaymentController
{
    private readonly IPaymentMethodService _paymentMethodService;
    private readonly IPaymentTypeService _paymentTypeService;

    public PaymentController(IPaymentMethodService paymentMethodService, IPaymentTypeService paymentTypeService)
    {
        _paymentMethodService = paymentMethodService;
        _paymentTypeService = paymentTypeService;
    }
    [HttpGet]
    public async <> GetPayment
}