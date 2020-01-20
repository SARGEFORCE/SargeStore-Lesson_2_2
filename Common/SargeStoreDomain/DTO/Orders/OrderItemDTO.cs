using SargeStoreDomain.Entities.Base;

namespace SargeStoreDomain.DTO.Orders
{
    public class OrderItemDTO : BaseEntity
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
