using System.Reflection.Metadata;

namespace Order.Contract.Services;

public interface IOrderService
{
    Task<List<ApplicationCore.Entities.Order>> GetAllOrders();
    Task<ApplicationCore.Entities.Order> GetOrdersByCustomerId(int customerId);
    Task<ApplicationCore.Entities.Order>SaveOrder(Models.OrderModal order);
    Task<ApplicationCore.Entities.Order> UpdateOrder(Models.OrderModal order);
   Task<ApplicationCore.Entities.Order> DeleteOrder(int orderId);
}