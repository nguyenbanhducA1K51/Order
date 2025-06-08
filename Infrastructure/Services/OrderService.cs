using Order.Contract.Services;
using Order.Models;

namespace Infrastructure.Services;

public class OrderService: IOrderService
{
    public Task<List<ApplicationCore.Entities.Order>> GetAllOrders()
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationCore.Entities.Order> GetOrdersByCustomerId(int customerId)
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationCore.Entities.Order> SaveOrder(OrderModal order)
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationCore.Entities.Order> UpdateOrder(OrderModal order)
    {
        throw new NotImplementedException();
    }

    public Task<ApplicationCore.Entities.Order> DeleteOrder(int orderId)
    {
        throw new NotImplementedException();
    }
}