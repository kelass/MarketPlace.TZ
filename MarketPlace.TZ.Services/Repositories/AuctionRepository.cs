using AutoMapper;
using MarketPlace.TZ.Data;
using MarketPlace.TZ.Domain.DbModels;
using MarketPlace.TZ.Domain.DtoModels;
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

        public async Task<bool> CreateAsync(AuctionDto entity)
        {
            Auction? auction = await _context.Auctions.FindAsync(entity.Id);
            if (auction == null)
            {
                await _context.Auctions.AddAsync(_mapper.Map<Auction>(entity));
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            Auction? auction = await _context.Auctions.FindAsync(Id);

            if (auction == null)
            {
                return false;
            }

            _context.Remove(auction);
            return true;
        }

        public async Task<Auction> GetByIdAsync(int Id)
        {
            return await _context.Auctions.FindAsync(Id);
        }

        public Task<IEnumerable<Auction>> PaginationSelectAsync(int index)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Auction>> SelectAsync()
        {
            return await _context.Auctions.ToListAsync();
        }
    }
}
