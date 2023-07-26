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
        /// Filtring and sorting auctions from db
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Auction>> Select([FromQuery]SortFilterDto entity)
        {
            IEnumerable<Auction> auctions = await _unitOfWork.Auctions.SortAsync(entity.SortKey, entity.Direction, entity.Limit);
            return await _unitOfWork.Auctions.Filtring(auctions, entity.Key, entity.Value);
        }
    }
}
