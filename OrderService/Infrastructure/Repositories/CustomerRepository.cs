using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Order.Contract.Repositories;

namespace Infrastructure.Repositories;

public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
{
    public CustomerRepository(OrderDbContext dbContext) : base(dbContext)
    {

    }

    public async Task<Customer> GetByIdAsync(int id)
    {
        return await _dbContext.Customers
            .Include(c => c.UserAddresses)
            .ThenInclude(ua => ua.Address)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Customer> CreateCustomerAsync(Customer customer)
    {
        _dbContext.Customers.Add(customer);
        await _dbContext.SaveChangesAsync(); // Save to generate the ID
        return customer;
    }

    public async Task<List<Customer>> GetAllAsync()
    {
        return await _dbContext.Customers
            .Include(c => c.UserAddresses)
            .ThenInclude(ua => ua.Address)
            .ToListAsync();
    }
    
}

