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
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            Item? item = await _context.Items.FindAsync(Id);

            if (item == null)
            {
                return false;
            }

            _context.Remove(item);
            return true;
        }

        public async Task<Item> GetByIdAsync(int Id)
        {
            return await _context.Items.FindAsync(Id);
        }

        public async Task<IEnumerable<Item>> PaginationSelectAsync(int index)
        {
            return await _context.Items.Skip(10*index).Take(10).ToListAsync();
        }

        public async Task<IEnumerable<Item>> SelectAsync()
        {
            return await _context.Items.ToListAsync();
        }
    }
}
