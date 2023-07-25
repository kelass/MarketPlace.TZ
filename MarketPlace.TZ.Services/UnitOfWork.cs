using AutoMapper;
using MarketPlace.TZ.Data;
using MarketPlace.TZ.Services.Interfaces;
using MarketPlace.TZ.Services.Repositories;
using Microsoft.Extensions.Logging;

namespace MarketPlace.TZ.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ItemRepository> _loggerItems;
        private readonly ILogger<AuctionRepository> _loggerAuctions;

        private IItemRepository _itemRepository;
        private IAuctionRepository _auctionRepository;
        public UnitOfWork(ApplicationDbContext context,
            IMapper mapper,
            ILogger<AuctionRepository> loggerAuctions,
            ILogger<ItemRepository> loggerItems)
        {
            _context = context;
            _mapper = mapper;
            _loggerAuctions = loggerAuctions;
            _loggerItems = loggerItems;
        }

        public IItemRepository Items
        {
            get
            {
                return _itemRepository = _itemRepository ?? new ItemRepository(_context, _mapper, _loggerItems);
            }
        }
        public IAuctionRepository Auctions
        {
            get
            {
                return _auctionRepository = _auctionRepository ?? new AuctionRepository(_context, _mapper, _loggerAuctions);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
