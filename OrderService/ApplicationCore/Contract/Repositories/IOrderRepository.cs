namespace Order.Contract.Repositories;

public interface IOrderRepository:IBaseRepository<ApplicationCore.Entities.Order>
{
    Task<List<ApplicationCore.Entities.Order>> GetAllOrders();
    Task<ApplicationCore.Entities.Order> GetOrdersByCustomerId(int customerId);
    Task<ApplicationCore.Entities.Order>SaveOrder(ApplicationCore.Entities.Order order );
    Task<ApplicationCore.Entities.Order> UpdateOrder(ApplicationCore.Entities.Order order);
    Task<ApplicationCore.Entities.Order> DeleteOrder(int orderId);
}