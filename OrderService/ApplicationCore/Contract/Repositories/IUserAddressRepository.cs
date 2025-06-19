using ApplicationCore.Entities;

namespace Order.Contract.Repositories;

public interface IUserAddressRepository:IBaseRepository<User_Address>
{
    Task<User_Address> SaveUserAddress(User_Address addressEntity);
}