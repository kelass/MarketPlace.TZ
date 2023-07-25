using MarketPlace.TZ.Domain.DbModels;
using MarketPlace.TZ.Domain.Enums;

namespace MarketPlace.TZ.Domain.DtoModels
{
    public class AuctionDto
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public DateTime CreateDt { get; set; }
        public DateTime? FinishedDt { get; set; }
        public decimal Price { get; set; }
        public MarketStatus Status { get; set; }
        public string Seller { get; set; }
        public string Buyer { get; set; }
    }
}
