using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Order.Contract.Repositories;

namespace Infrastructure.Repositories;

public class BaseRepository<T>: IBaseRepository<T> where T: class
{
    protected readonly OrderDbContext _dbContext;
    public BaseRepository(OrderDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async  Task<T> Insert(T entity)
    { 
        await _dbContext.Set<T>().AddAsync(entity);      // ✅ async add
        await _dbContext.SaveChangesAsync();             // ✅ async save
        return entity;
    }

    public async Task<T> DeleteById(int id) 
    {
        var entity = await _dbContext.Set<T>().FindAsync(id);

        if (entity is null)                       // not found → nothing to delete
            return null;
        
        _dbContext.Set<T>().Remove(entity);

    await _dbContext.SaveChangesAsync();
    
        return entity;
    }
    public async Task<T> Update(T entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task< IEnumerable<T>> GetAll()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }

    public async Task<T> GetById(int id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }
}