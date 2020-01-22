using SargeStoreDomain.DTO.Orders;
using System.Collections.Generic;

namespace SargeStore.Interfaces.Services
{
    public interface IOrderService
    {
        IEnumerable<OrderDTO> GetUserOrders(string UserName);
        OrderDTO GetOrderById(int id);
        OrderDTO CreateOrder(CreateOrderModel OrderModel, string UserName);
    }
}
