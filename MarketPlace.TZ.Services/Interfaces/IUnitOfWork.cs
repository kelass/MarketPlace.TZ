using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.TZ.Services.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IItemRepository Items { get; }
        IAuctionRepository Auctions { get; }
        Task SaveAsync();
    }
}
