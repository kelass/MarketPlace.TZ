using MarketPlace.TZ.Domain.DbModels;
using MarketPlace.TZ.Domain.DtoModels;
using MarketPlace.TZ.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.TZ.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public AuctionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Select all auctions from db without pagination
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<IEnumerable<Auction>> Select()
        {
            return _unitOfWork.Auctions.SelectAsync();
        }

        /// <summary>
        /// Select all auctions from db with pagination (index page)
        /// </summary>
        /// <returns></returns>
        [HttpGet("{index}")]
        public async Task<IEnumerable<Auction>> PaginationSelect(int index)
        {
            return await _unitOfWork.Auctions.PaginationWithIndexAsync(index);
        }

        /// <summary>
        /// Get auction by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<Auction> GetById(int id)
        {
            return await _unitOfWork.Auctions.GetByIdAsync(id);
        }

        /// <summary>
        /// Add auction to db
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Create(AuctionDto entity)
        {
            bool isSuccess = await _unitOfWork.Auctions.CreateAsync(entity);
            await _unitOfWork.SaveAsync();
            _unitOfWork.Dispose();
            return isSuccess;
        }

        /// <summary>
        /// Delete auction from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> Delete(int id)
        {
            bool isSuccess = await _unitOfWork.Auctions.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
            _unitOfWork.Dispose();
            return isSuccess;
        }

        /// <summary>
        /// Sorted auctions from db
        /// </summary>
        /// <param name="sortKey"></param>
        /// <param name="direction"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Auction>> Sort(string sortKey, string direction, int limit)
        {
            return await _unitOfWork.Auctions.SortAsync(sortKey, direction, limit);
        }

        /// <summary>
        /// Filtring auctions from db
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<IEnumerable<Auction>> Filter(string key, string value)
        {
            return _unitOfWork.Auctions.Filtring(key, value);
        }

        /// <summary>
        /// Filtring and sorting auctions from db
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Auction>> SortFilter([FromQuery]SortFilterDto entity)
        {
            IEnumerable<Auction> auctions = await _unitOfWork.Auctions.Filtring(entity.Key, entity.Value);
            return await _unitOfWork.Auctions.SortFilter(auctions, entity.SortKey, entity.Direction, entity.Limit);
        }
    }
}
