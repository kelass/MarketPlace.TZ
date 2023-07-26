using AutoMapper;
using MarketPlace.TZ.Domain.DbModels;
using MarketPlace.TZ.Domain.DtoModels;

namespace MarketPlace.TZ.Services.Mapping
{
    internal class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ItemDto, Item>();
            CreateMap<AuctionDto, Auction>();
        }
    }
}
