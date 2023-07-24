using MarketPlace.TZ.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.TZ.Domain.DbModels
{
    public class Auction
    {
        public int Id { get; set; }
        public Item Item { get; set; }
        public DateTime CreateDt { get; set; }
        public DateTime FinishedDt { get; set; }
        public decimal Price { get; set; }
        public MarketStatus Status { get; set; }
        public string Seller { get; set; }
        public string Buyer { get; set; }
    }
}
