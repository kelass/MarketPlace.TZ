using AutoMapper;
using MarketPlace.TZ.Data;
using MarketPlace.TZ.Domain.DbModels;
using MarketPlace.TZ.Domain.DtoModels;
using MarketPlace.TZ.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MarketPlace.TZ.Services.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public ItemRepository(ApplicationDbContext context, IMapper mapper, ILogger<ItemRepository> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Item> GetByIdAsync(int Id)
        {
            return await _context.Items.FindAsync(Id);
        }
        public async Task<IEnumerable<Item>> SearchByNameAsync(string name)
        {
            return await _context.Items.Where(item=>item.Name == name).ToListAsync();
        }
    }
}
