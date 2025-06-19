using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Order.Contract.Repositories;

namespace Infrastructure.Repositories;

public class OrderRepository :  BaseRepository<ApplicationCore.Entities.Order>,IOrderRepository 
{
    
    public OrderRepository(OrderDbContext dbContext): base(dbContext)
    {
    }
    public async Task<List<ApplicationCore.Entities.Order>> GetAllOrders()
    {
        return await _dbContext.Orders
            .Include(o => o.Order_Details)
            .ToListAsync();
    }

    public async Task<ApplicationCore.Entities.Order> GetOrdersByCustomerId(int customerId)
    {
      return await _dbContext.Orders
          .Include(o => o.Order_Details)
          .FirstOrDefaultAsync(o => o.Customer_Id == customerId);
       
    }

    public async Task<ApplicationCore.Entities.Order> SaveOrder(ApplicationCore.Entities.Order order)
    {
        await _dbContext.Orders.AddAsync(order);
        await _dbContext.SaveChangesAsync();
        return order;
    }

    public async Task<ApplicationCore.Entities.Order> UpdateOrder(ApplicationCore.Entities.Order order)
    {
        _dbContext.Orders.Update(order);
        await _dbContext.SaveChangesAsync();
        return order;
    }

    public async Task<ApplicationCore.Entities.Order> DeleteOrder(int orderId)
    {
        var order = await _dbContext.Orders.FindAsync(orderId);
        if (order == null) return null;

        _dbContext.Orders.Remove(order);
        await _dbContext.SaveChangesAsync();
        return order;
    }
}