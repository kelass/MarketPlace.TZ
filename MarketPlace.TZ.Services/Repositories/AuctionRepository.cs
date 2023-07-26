using AutoMapper;
using MarketPlace.TZ.Data;
using MarketPlace.TZ.Domain.DbModels;
using MarketPlace.TZ.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MarketPlace.TZ.Services.Repositories
{
    public class AuctionRepository : IAuctionRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        ILogger<AuctionRepository> _logger;
        public AuctionRepository(ApplicationDbContext context, IMapper mapper, ILogger<AuctionRepository> logger)
        {
            _mapper = mapper;
            _context = context;
            _logger = logger;
        }
        public Task<IEnumerable<Auction>> Filtring(IEnumerable<Auction> auctions,string key, string value)
        {
            IEnumerable<Auction> list = new List<Auction>();

            if (key != null && value !=null)
            {
                switch (key.ToLower())
                {
                    case "status":
                        try
                        {
                            int status = Convert.ToInt32(value);
                            list = auctions.Where(auction => (int)auction.Status == status);
                            break;
                        }
                        catch (FormatException ex)
                        {
                            _logger.LogError($"Data not filtred by:{key}. Exception:{ex.Message}");
                            return Task.FromResult(list);
                        }

                    case "seller":
                        list = auctions.Where(auction => auction.Seller == value);
                        break;
                }

                _logger.LogInformation($"Data filtred by:{key}");
                return Task.FromResult(list);
            }
            _logger.LogError($"Data not filtred by:{key}");
            return Task.FromResult(auctions);
        }

        public async Task<Auction> GetByIdAsync(int Id)
        {
            return await _context.Auctions.FindAsync(Id);
        }

        public Task<IEnumerable<Auction>> SortAsync(string sortKey, string direction, int limit)
        {
            IEnumerable<Auction> auctions = new List<Auction>();

            if (sortKey != null)
            {
                if ((direction == "abs") || (direction == "ABS"))
                    switch (sortKey.ToLower())
                    {
                        case "id": auctions = _context.Auctions.Include(auction=>auction.Item).OrderBy(auction => auction.Id); break;
                        case "itemid": auctions = _context.Auctions.Include(auction => auction.Item).OrderBy(auction => auction.Item.Id); break;
                        case "createdt": auctions = _context.Auctions.Include(auction => auction.Item).OrderBy(auction => auction.CreateDt); break;
                        case "finisheddt": auctions = _context.Auctions.Include(auction => auction.Item).OrderBy(auction => auction.FinishedDt); break;
                        case "price": auctions = _context.Auctions.Include(auction => auction.Item).OrderBy(auction => auction.Price); break;
                        case "status": auctions = _context.Auctions.Include(auction => auction.Item).OrderBy(auction => auction.Status); break;
                    }

                else
                    switch (sortKey.ToLower())
                    {
                        case "id": auctions = _context.Auctions.Include(auction => auction.Item).OrderByDescending(auction => auction.Id); break;
                        case "itemid": auctions = _context.Auctions.Include(auction => auction.Item).OrderByDescending(auction => auction.Item.Id); break;
                        case "createdt": auctions = _context.Auctions.Include(auction => auction.Item).OrderByDescending(auction => auction.CreateDt); break;
                        case "finisheddt": auctions = _context.Auctions.Include(auction => auction.Item).OrderByDescending(auction => auction.FinishedDt); break;
                        case "price": auctions = _context.Auctions.Include(auction => auction.Item).OrderByDescending(auction => auction.Price); break;
                        case "status": auctions = _context.Auctions.Include(auction => auction.Item).OrderByDescending(auction => auction.Status); break;
                    }

            }
            _logger.LogInformation($"Data sorted by:{direction} and limited {limit}");

            return Task.FromResult(auctions.Take(limit));
        }
    }
}
