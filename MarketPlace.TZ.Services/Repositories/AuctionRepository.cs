using AutoMapper;
using MarketPlace.TZ.Data;
using MarketPlace.TZ.Domain.DbModels;
using MarketPlace.TZ.Domain.DtoModels;
using MarketPlace.TZ.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

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

        public async Task<bool> CreateAsync(AuctionDto entity)
        {
            Auction? auction = await _context.Auctions.FindAsync(entity.Id);
            Item? item = await _context.Items.FindAsync(entity.ItemId);
            if (auction == null && item != null)
            {
                //TODO переделать
                Auction a = _mapper.Map<Auction>(entity);
                a.Item = item;

                await _context.Auctions.AddAsync(a);
                _logger.LogInformation($"Auction with Id:{entity.Id} created");
                return true;
            }
            _logger.LogError($"Auction with Id:{entity.Id} not created");
            return false;
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            Auction? auction = await _context.Auctions.FindAsync(Id);

            if (auction == null)
            {
                _logger.LogError($"Auction with Id:{Id} not deleted");
                return false;
            }
            _context.Remove(auction);
            _logger.LogInformation($"Auction with Id:{Id} deleted");
            return true;
        }

        public async Task<Auction> GetByIdAsync(int Id)
        {
            return await _context.Auctions.FindAsync(Id);
        }

        public async Task<IEnumerable<Auction>> PaginationWithIndexAsync(int index)
        {
            return await _context.Auctions.Skip(10 * index).Take(10).ToListAsync();
        }

        public async Task<IEnumerable<Auction>> PaginationWithPageLimitAndIndexAsync(int index, int limit)
        {
            return await _context.Auctions.Skip(index * limit).Take(limit).ToListAsync();
        }

        public async Task<IEnumerable<Auction>> PaginationWithPageLimitAsync(int limit)
        {
            return await _context.Auctions.Take(limit).ToListAsync();
        }

        public async Task<IEnumerable<Auction>> SelectAsync()
        {
            return await _context.Auctions.ToListAsync();
        }

        public Task<IEnumerable<Auction>> SortAsync(string sortKey, string direction, int limit)
        {
            IEnumerable<Auction> auctions = new List<Auction>();

            if (sortKey != null)
            {
                if ((direction == "abs") || (direction == "ABS"))
                    switch (sortKey.ToLower())
                    {
                        case "id": auctions = _context.Auctions.OrderBy(auction => auction.Id); break;
                        case "itemid": auctions = _context.Auctions.Include(auction => auction.Item).OrderBy(auction => auction.Item.Id); break;
                        case "createdt": auctions = _context.Auctions.OrderBy(auction => auction.CreateDt); break;
                        case "finisheddt": auctions = _context.Auctions.OrderBy(auction => auction.FinishedDt); break;
                        case "price": auctions = _context.Auctions.OrderBy(auction => auction.Price); break;
                        case "status": auctions = _context.Auctions.OrderBy(auction => auction.Status); break;
                    }

                else
                    switch (sortKey.ToLower())
                    {
                        case "id": auctions = _context.Auctions.OrderByDescending(auction => auction.Id); break;
                        case "itemid": auctions = _context.Auctions.Include(auction => auction.Item).OrderByDescending(auction => auction.Item.Id); break;
                        case "createdt": auctions = _context.Auctions.OrderByDescending(auction => auction.CreateDt); break;
                        case "finisheddt": auctions = _context.Auctions.OrderByDescending(auction => auction.FinishedDt); break;
                        case "price": auctions = _context.Auctions.OrderByDescending(auction => auction.Price); break;
                        case "status": auctions = _context.Auctions.OrderByDescending(auction => auction.Status); break;
                    }

            }
            _logger.LogInformation($"Data sorted by:{direction} and limited {limit}");

            return Task.FromResult(auctions.Take(limit));
        }
    }
}
