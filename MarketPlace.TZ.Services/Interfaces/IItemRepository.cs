using MarketPlace.TZ.Domain.DbModels;
using MarketPlace.TZ.Domain.DtoModels;

namespace MarketPlace.TZ.Services.Interfaces
{
    public interface IItemRepository:IRepository<Item>
    {
        Task<IEnumerable<Item>> SearchByNameAsync(string name);
        
    }
}
