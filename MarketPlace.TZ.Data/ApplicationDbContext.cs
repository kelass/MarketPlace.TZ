using MarketPlace.TZ.Domain.DbModels;
using Microsoft.EntityFrameworkCore;

namespace MarketPlace.TZ.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
            Database.Migrate();
        }
    }
}
