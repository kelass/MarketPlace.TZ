using MarketPlace.TZ.Domain.DbModels;
using MarketPlace.TZ.Domain.DtoModels;

namespace MarketPlace.TZ.Services.Interfaces
{
    public interface IAuctionRepository:IRepository<Auction>
    {
        Task<bool> CreateAsync(AuctionDto entity);

        Task<IEnumerable<Auction>> SortAsync(string sortKey, string direction, int limit);
        Task<IEnumerable<Auction>> Filtring(string key, string value);
        Task<IEnumerable<Auction>> SortFilter(IEnumerable<Auction> auctions,string sortKey, string direction, int limit);
    }
}
