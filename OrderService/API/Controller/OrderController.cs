using System.Net.Mime;
using Order.Contract.Services;
using Order.Models;

namespace API.Controller;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using Order.Contract.Repositories; 

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
    [HttpGet("GetAllOrders")]
    public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
    {
        var orders = await _orderService.GetAllOrders();
        return Ok(orders);
    }
    
    [HttpPost("SaveOrder")]
    public async Task<ActionResult<OrderDetailsModal>> SaveOrder([FromBody] OrderModal order )
    {
        if (order == null) return BadRequest();

        var savedOrder = await _orderService.SaveOrder(order);
        return CreatedAtAction(nameof(GetOrderByCustomerId), new { customerId = savedOrder.CustomerId }, savedOrder);
    }
    
    [HttpGet("GetOrderByCustomerId/{customerId}")]
    public async Task<ActionResult<Order>> GetOrderByCustomerId(int customerId)
    {
        var order = await _orderService.GetOrdersByCustomerId(customerId);
        if (order == null) return NotFound();

        return Ok(order);
    }
    
    [HttpDelete("DeleteOrder/{orderId}")]
    public async Task<IActionResult> DeleteOrder(int orderId)
    {
        var deletedOrder = await _orderService.DeleteOrder(orderId);
        if (deletedOrder == null) return NotFound();

        return NoContent();
    }
    
    [HttpPut("UpdateOrder/{orderId}")]
    public async Task<ActionResult<OrderDetailsModal>> UpdateOrder(int orderId, [FromBody] OrderModal order)
    {
        if (order == null || orderId==null)
            return BadRequest();

        var updatedOrder = await _orderService.UpdateOrder(order, orderId);
        if (updatedOrder == null) return NotFound();

        return Ok(updatedOrder);
    }
}