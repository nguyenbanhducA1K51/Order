using Infrastructure.Data;
using Order.Contract.Services;

namespace Infrastructure.Services;

public class TransactionService : ITransactionService
{
    private readonly OrderDbContext _dbContext;

    public TransactionService(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<T> ExecuteInTransactionAsync<T>(Func<Task<T>> operation)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var result = await operation();
            await transaction.CommitAsync();
            return result;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task ExecuteInTransactionAsync(Func<Task> operation)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            await operation();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}