
namespace MarketPlace.TZ.Services.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(int Id);
    }
}
