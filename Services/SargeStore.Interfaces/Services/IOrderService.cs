using SargeStoreDomain.Entities;
using SargeStoreDomain.ViewModels;
using System.Collections.Generic;

namespace SargeStore.Interfaces.Services
{
    public interface IOrderService
    {
        IEnumerable<Order> GetUserOrders(string UserName);
        Order GetOrderById(int id);
        Order CreateOrder(OrderViewModel OrderModel, CartViewModel CartModel, string UserName);
    }
}
