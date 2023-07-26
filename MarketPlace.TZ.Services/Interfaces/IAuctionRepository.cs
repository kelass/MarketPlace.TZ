using MarketPlace.TZ.Domain.DbModels;
using MarketPlace.TZ.Domain.DtoModels;

namespace MarketPlace.TZ.Services.Interfaces
{
    public interface IAuctionRepository:IRepository<Auction>
    {
        Task<IEnumerable<Auction>> SortAsync(string sortKey, string direction, int limit);
        Task<IEnumerable<Auction>> Filtring(IEnumerable<Auction> auctions, string key, string value);
    }
}
