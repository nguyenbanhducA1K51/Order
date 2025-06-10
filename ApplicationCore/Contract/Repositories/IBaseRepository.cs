namespace Order.Contract.Repositories;

public interface IBaseRepository<T> where T: class
{
  
    
        Task<T> Insert(T entity);
        Task<T> DeleteById(int id);
        Task<T> Update(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
}