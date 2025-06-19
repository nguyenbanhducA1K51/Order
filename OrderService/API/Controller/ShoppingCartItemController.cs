using Microsoft.AspNetCore.Mvc;
using Order.Contract.Services;

namespace API.Controller;

[ApiController]
[Route("api/[controller]")]
public class ShoppingCartItemController:ControllerBase 
{
    private readonly IShoppingCartItemService _shoppingCartItemService;

    public ShoppingCartItemController(IShoppingCartItemService shoppingCartItemService)
    {
        _shoppingCartItemService = shoppingCartItemService;
    }

    [HttpDelete("DeleteShoppingCart/{id}")]
    public async Task<IActionResult> DeleteShoppingCartItem(int id)
    {
        return NoContent();
    }
}