using ApplicationCore.Entities;
using Infrastructure.Data;
using Order.Contract.Repositories;

namespace Infrastructure.Repositories;

public class UserAddressRepository:BaseRepository<User_Address>,IUserAddressRepository
{
    public UserAddressRepository(OrderDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<User_Address> SaveUserAddress(User_Address addressEntity)
    {
        await _dbContext.User_Address.AddAsync(addressEntity);
        await _dbContext.SaveChangesAsync();
        return addressEntity;
    }
}