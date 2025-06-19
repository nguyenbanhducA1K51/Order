namespace Order.Contract.Repositories;

public interface ICustomerRepository:IBaseRepository<ApplicationCore.Entities.Customer>
{
    Task<ApplicationCore.Entities.Customer> CreateCustomerAsync(ApplicationCore.Entities.Customer customer);
    Task<List< ApplicationCore.Entities.Customer>> GetAllAsync();
    Task<ApplicationCore.Entities.Customer> GetByIdAsync(int customerId);
}