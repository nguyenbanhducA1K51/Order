using ApplicationCore.Entities;
using Infrastructure.Data;
using Order.Contract.Repositories;

namespace Infrastructure.Repositories;

public class AddressRepository:BaseRepository<ApplicationCore.Entities.Address>,IAddressRepository
{
    public AddressRepository(OrderDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Address> SaveAddressAsync(Address address)
    {
        _dbContext.Addresses.Add(address);
        await _dbContext.SaveChangesAsync(); // Save to generate the ID
        return address;
    }
}