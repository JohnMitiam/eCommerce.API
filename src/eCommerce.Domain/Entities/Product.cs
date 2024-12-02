using eCommerce.Domain.Entities.Base;

namespace eCommerce.Domain.Entities
{
    public class Product: BaseModel
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public decimal? ItemStocks {  get; set; }
        public bool? IsActive { get; set; }
    }
}
