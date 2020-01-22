using Microsoft.Extensions.Configuration;
using SargeStore.Clients.Base;
using SargeStore.Interfaces.Services;
using SargeStoreDomain.DTO.Orders;
using System.Collections.Generic;
using System.Net.Http;

namespace SargeStore.Clients.Orders
{
    public class OrdersClient : BaseClient, IOrderService
    {
        public OrdersClient(IConfiguration Configuration) : base(Configuration, "api/Orders") { }

        public OrderDTO CreateOrder(CreateOrderModel OrderModel, string UserName) => Post($"{_ServiceAddress}/{UserName}", OrderModel)
            .Content.ReadAsAsync<OrderDTO>().Result;

        public OrderDTO GetOrderById(int id) => Get<OrderDTO>($"{_ServiceAddress}/{id}");

        public IEnumerable<OrderDTO> GetUserOrders(string UserName) => Get<List<OrderDTO>>($"{_ServiceAddress}/user/{UserName}");
    }
}
