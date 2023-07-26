using MarketPlace.TZ.Domain.DbModels;
using MarketPlace.TZ.Domain.DtoModels;
using MarketPlace.TZ.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketPlace.TZ.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ItemController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Selected all entities in db without pagination
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<IEnumerable<Item>> Select()
        {
            return _unitOfWork.Items.SelectAsync();
        }

        /// <summary>
        /// Selected all entities in db with pagination
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        [HttpGet("{index}")]
        public async Task<IEnumerable<Item>> PaginationSelect(int index)
        {
            return await _unitOfWork.Items.PaginationWithIndexAsync(index);
        }

        /// <summary>
        /// Select item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<Item> GetById(int id)
        {
            return await _unitOfWork.Items.GetByIdAsync(id);
        }

        /// <summary>
        /// Create item to db
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> Create(ItemDto entity)
        {
            bool isSuccess = await _unitOfWork.Items.CreateAsync(entity);
            await _unitOfWork.SaveAsync();
            _unitOfWork.Dispose();
            return isSuccess;
        }

        /// <summary>
        /// Delete item from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<bool> Delete(int id)
        {
            bool isSuccess = await _unitOfWork.Items.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
            _unitOfWork.Dispose();
            return isSuccess;
        }

        /// <summary>
        /// Search by name without pagination
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        public async Task<IEnumerable<Item>> SearchByName(string name)
        {
            return await _unitOfWork.Items.SearchByNameAsync(name);
        }

        /// <summary>
        /// Pagination method (using limit)
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet("{limit}")]
        public async Task<IEnumerable<Item>> Limit(int limit)
        {
            return await _unitOfWork.Items.PaginationWithPageLimitAsync(limit);
        }

        /// <summary>
        /// Pagination method (using limit and index page)
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Item>> LimitPageIndex(int limit, int index)
        {
            return await _unitOfWork.Items.PaginationWithPageLimitAndIndexAsync(index,limit);
        }
        [HttpGet]
        public async Task<IEnumerable<Item>> FilterByName(string name)
        {
            return await _unitOfWork.Items.FilterByName(name);
        }
    }
}
