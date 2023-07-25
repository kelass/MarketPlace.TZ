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

        [HttpGet]
        public Task<IEnumerable<Auction>> Select()
        {
            return _unitOfWork.Auctions.SelectAsync();
        }

        [HttpGet("{index}")]
        public async Task<IEnumerable<Auction>> PaginationSelect(int index)
        {
            return await _unitOfWork.Auctions.PaginationWithIndexAsync(index);
        }

        [HttpGet("{id}")]
        public async Task<Auction> GetById(int id)
        {
            return await _unitOfWork.Auctions.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<bool> Create(AuctionDto entity)
        {
            bool isSuccess = await _unitOfWork.Auctions.CreateAsync(entity);
            await _unitOfWork.SaveAsync();
            _unitOfWork.Dispose();
            return isSuccess;
        }

        [HttpDelete]
        public async Task<bool> Delete(int id)
        {
            bool isSuccess = await _unitOfWork.Auctions.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
            _unitOfWork.Dispose();
            return isSuccess;
        }

        [HttpGet]
        public async Task<IEnumerable<Auction>> Sort(string sortKey, string direction, int limit)
        {
            return await _unitOfWork.Auctions.SortAsync(sortKey, direction, limit);
        }
    }
}
