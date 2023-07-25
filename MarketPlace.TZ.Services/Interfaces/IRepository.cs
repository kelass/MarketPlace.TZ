
namespace MarketPlace.TZ.Services.Interfaces
{
    public interface IRepository<T>
    {
        Task<bool> DeleteAsync(int Id);
        Task<T> GetByIdAsync(int Id);
        Task<IEnumerable<T>> SelectAsync();
        Task<IEnumerable<T>> PaginationSelectAsync(int index);
    }
}
