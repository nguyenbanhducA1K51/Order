using System.Reflection.Metadata;
using Order.Models;

namespace Order.Contract.Services;

public interface IOrderService
{
    Task<List<OrderDetailsModal>> GetAllOrders();
    Task<OrderDetailsModal> GetOrdersByCustomerId(int customerId);
    Task<OrderDetailsModal>SaveOrder(Models.OrderModal order);
    Task<OrderDetailsModal> UpdateOrder(Models.OrderModal order, int orderId);
   Task<OrderModal> DeleteOrder(int orderId);
}