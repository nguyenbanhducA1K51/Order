using System.Net.Mime;

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
    private readonly IOrderRepository _orderRepository;

    public OrderController(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders()
    {
        var orders = await _orderRepository.GetAllOrders();
        return Ok(orders);
    }
    
    [HttpPost]
    public async Task<ActionResult<Order>> SaveOrder([FromBody] Order order)
    {
        if (order == null) return BadRequest();

        var savedOrder = await _orderRepository.SaveOrder(order);
        return CreatedAtAction(nameof(GetOrderByCustomerId), new { customerId = savedOrder.CustomerId }, savedOrder);
    }
    
    [HttpGet("customer/{customerId}")]
    public async Task<ActionResult<Order>> GetOrderByCustomerId(int customerId)
    {
        var order = await _orderRepository.GetOrdersByCustomerId(customerId);
        if (order == null) return NotFound();

        return Ok(order);
    }
    
    [HttpDelete("{orderId}")]
    public async Task<IActionResult> DeleteOrder(int orderId)
    {
        var deletedOrder = await _orderRepository.DeleteOrder(orderId);
        if (deletedOrder == null) return NotFound();

        return NoContent();
    }
    
    [HttpPut("{orderId}")]
    public async Task<ActionResult<Order>> UpdateOrder(int orderId, [FromBody] Order order)
    {
        if (order == null || order.Id != orderId)
            return BadRequest();

        var updatedOrder = await _orderRepository.UpdateOrder(order);
        if (updatedOrder == null) return NotFound();

        return Ok(updatedOrder);
    }
}