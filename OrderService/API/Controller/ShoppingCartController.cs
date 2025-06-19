using Microsoft.AspNetCore.Mvc;
using Order.Contract.Services;

namespace API.Controller;
[ApiController]
[Route("api/[controller]")]
public class ShoppingCartController:ControllerBase
{
    private readonly IShoppingCartService _shoppingCartService;

    public ShoppingCartController(IShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService;
    }
[HttpGet("GetShoppingCartByCustomerId/{customerId}")]
    public async Task<IActionResult> GetShoppingCartByCustomerId()
    {
        return NoContent();
    }

    [HttpPost("SaveShoppingCart")]
    public async Task<IActionResult> SaveShoppingCart()
    {
        return NoContent();
    }

    [HttpDelete("DeleteShoppingCart/{id}")]
    public async Task<IActionResult> DeleteShoppingCart(int id)
    {
        return NoContent();
    }
}