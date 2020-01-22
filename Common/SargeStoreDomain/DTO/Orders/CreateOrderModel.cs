using SargeStoreDomain.ViewModels;
using System.Collections.Generic;
using System.Text;

namespace SargeStoreDomain.DTO.Orders
{
    public class CreateOrderModel
    {
        public OrderViewModel OrderViewModel { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
