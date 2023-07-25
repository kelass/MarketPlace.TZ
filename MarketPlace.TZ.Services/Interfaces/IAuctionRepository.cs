using MarketPlace.TZ.Domain.DbModels;
using MarketPlace.TZ.Domain.DtoModels;

namespace MarketPlace.TZ.Services.Interfaces
{
    public interface IAuctionRepository:IRepository<Auction>
    {
        Task<bool> CreateAsync(AuctionDto entity);
    }
}
