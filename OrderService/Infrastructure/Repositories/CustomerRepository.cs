using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Order.Contract.Repositories;

namespace Infrastructure.Repositories;

public class CustomerRepository:BaseRepository<Customer>, ICustomerRepository
{
public CustomerRepository(OrderDbContext dbContext) : base(dbContext)
{
    
}

public async  Task<Customer> GetById(int id)
{
    return await _dbContext.Customers
        .Include(c=>c.UserAddresses)
        .FirstOrDefaultAsync(c=>c.Id==id);
}

public async Task<Customer> CreateCustomerAsync(Customer customer)
{
    _dbContext.Customers.Add(customer);
    await _dbContext.SaveChangesAsync(); // Save to generate the ID
    return customer;
}

public Task<List<Customer>> GetAllCustomerAsync()
{
    return _dbContext.Customers.ToListAsync();
}
}