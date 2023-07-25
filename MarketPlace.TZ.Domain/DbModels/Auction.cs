using MarketPlace.TZ.Domain.Enums;

namespace MarketPlace.TZ.Domain.DbModels
{
    public class Auction
    {
        public int Id { get; set; }
        public Item Item { get; set; }
        public DateTime CreateDt { get; set; }
        public DateTime? FinishedDt { get; set; }
        public decimal Price { get; set; }
        public MarketStatus Status { get; set; }
        public string Seller { get; set; }
        public string Buyer { get; set; }
    }
}
