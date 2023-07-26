
namespace MarketPlace.TZ.Domain.DtoModels
{
    public class SortFilterDto
    {
        public int Limit { get; set; }
        public string? Key { get; set; }
        public string Direction { get; set; }
        public string SortKey { get; set; }
        public string? Value { get; set; }
    }
}
