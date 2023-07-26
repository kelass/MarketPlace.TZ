using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketPlace.TZ.Domain.DbModels
{
    public class SortFilterDto
    {
        public int Limit { get; set; }
        public string Key { get; set; }
        public string Direction { get; set; }
        public string SortKey { get; set; }
        public string Value { get; set; }
    }
}
