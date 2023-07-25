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

        [HttpGet]
        public Task<IEnumerable<Item>> Select()
        {
            return _unitOfWork.Items.SelectAsync();
        }

        [HttpGet("{index}")]
        public async Task<IEnumerable<Item>> PaginationSelect(int index)
        {
            return await _unitOfWork.Items.PaginationSelectAsync(index);
        }

        [HttpGet("{id}")]
        public async Task<Item> GetById(int id)
        {
            return await _unitOfWork.Items.GetByIdAsync(id);
        }

        [HttpPost]
        public async Task<bool> Create(ItemDto entity)
        {
            bool isSuccess = await _unitOfWork.Items.CreateAsync(entity);
            await _unitOfWork.SaveAsync();
            _unitOfWork.Dispose();
            return isSuccess;
        }

        [HttpDelete]
        public async Task<bool> Delete(int id)
        {
           bool isSuccess = await _unitOfWork.Items.DeleteAsync(id);
            await _unitOfWork.SaveAsync();
            _unitOfWork.Dispose();
            return isSuccess;
        }
    }
}
