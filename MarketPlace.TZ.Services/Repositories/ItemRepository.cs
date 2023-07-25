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

        public async Task<bool> CreateAsync(ItemDto entity)
        {
            Item? item = await _context.Items.FindAsync(entity.Id);
            if (item == null)
            {
                await _context.Items.AddAsync(_mapper.Map<Item>(entity));
                _logger.LogInformation($"Item with Id:{entity.Id} created");
                return true;
            }
            _logger.LogError($"Item not found");
            return false;
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            Item? item = await _context.Items.FindAsync(Id);

            if (item == null)
            {
                _logger.LogError($"Item not found");
                return false;
            }

            _context.Remove(item);
            _logger.LogInformation($"Item with Id:{Id} deleted");
            return true;
        }

        public async Task<Item> GetByIdAsync(int Id)
        {
            return await _context.Items.FindAsync(Id);
        }

        public async Task<IEnumerable<Item>> PaginationWithIndexAsync(int index)
        {
            return await _context.Items.Skip(10*index).Take(10).ToListAsync();
        }

        public async Task<IEnumerable<Item>> PaginationWithPageLimitAndIndexAsync(int index,int limit)
        {
            return await _context.Items.Skip(index*limit).Take(limit).ToListAsync();
        }

        public async Task<IEnumerable<Item>> PaginationWithPageLimitAsync(int limit)
        {
            return await _context.Items.Take(limit).ToListAsync();
        }

        public async Task<Item> SearchByNameAsync(string name)
        {
            return await _context.Items.FirstOrDefaultAsync(item=>item.Name == name);
        }

        public async Task<IEnumerable<Item>> SelectAsync()
        {
            return await _context.Items.ToListAsync();
        }
    }
}
