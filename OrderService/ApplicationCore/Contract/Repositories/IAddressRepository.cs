using ApplicationCore.Entities;

namespace Order.Contract.Repositories;

public interface IAddressRepository:IBaseRepository<ApplicationCore.Entities.Address>
{
    Task<Address> SaveAddressAsync(Address address);
    Task<Address> GetByIdAsync(int id);

}