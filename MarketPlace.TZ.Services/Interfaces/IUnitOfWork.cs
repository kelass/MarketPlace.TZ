
namespace MarketPlace.TZ.Services.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IItemRepository Items { get; }
        IAuctionRepository Auctions { get; }
        Task SaveAsync();
    }
}
