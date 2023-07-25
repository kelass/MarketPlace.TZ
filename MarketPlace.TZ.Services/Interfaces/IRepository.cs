
namespace MarketPlace.TZ.Services.Interfaces
{
    public interface IRepository<T>
    {
        Task<bool> DeleteAsync(int Id);
        Task<T> GetByIdAsync(int Id);
        Task<IEnumerable<T>> SelectAsync();
        Task<IEnumerable<T>> PaginationWithIndexAsync(int index);
        Task<IEnumerable<T>> PaginationWithPageLimitAsync(int limit);
        Task<IEnumerable<T>> PaginationWithPageLimitAndIndexAsync(int index,int limit);
    }
}
