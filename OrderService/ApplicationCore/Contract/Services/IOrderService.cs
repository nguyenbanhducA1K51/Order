using System.Reflection.Metadata;
using Order.Models;

namespace Order.Contract.Services;

public interface IOrderService
{
    Task<List<OrderModal>> GetAllOrders();
    Task<OrderModal> GetOrdersByCustomerId(int customerId);
    Task<OrderModal>SaveOrder(Models.OrderModal order);
    Task<OrderModal> UpdateOrder(Models.OrderModal order);
   Task<OrderModal> DeleteOrder(int orderId);
}