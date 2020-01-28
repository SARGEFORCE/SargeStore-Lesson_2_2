using DataAccessLayer.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SargeStoreDomain.Entities;
using SargeStoreDomain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using SargeStore.Interfaces.Services;
using SargeStoreDomain.DTO.Orders;
using Microsoft.Extensions.Logging;
using SargeStore.Services.Map;

namespace SargeStore.Services.FProduct
{
    public class SqlOrderService : IOrderService
    {
        private readonly SargeStoreDB _db;
        private readonly UserManager<User> _UserManager;
        private readonly ILogger<SqlOrderService> _Logger;

        public SqlOrderService(SargeStoreDB db, UserManager<User> UserManager, ILogger<SqlOrderService> Logger)
        {
            _db = db;
            _UserManager = UserManager;
            _Logger = Logger;
        }
        public OrderDTO CreateOrder(CreateOrderModel OrderModel, string UserName)
        {
            var user = _UserManager.FindByNameAsync(UserName).Result;
            using (var transaction = _db.Database.BeginTransaction())
            {
                var order = new Order
                {
                    Name = OrderModel.OrderViewModel.Name,
                    Address = OrderModel.OrderViewModel.Address,
                    Phone = OrderModel.OrderViewModel.Phone,
                    User = user,
                    Date = DateTime.Now
                };

                _db.Orders.Add(order);

                foreach (var item in OrderModel.OrderItems)
                {
                    var product = _db.Products.FirstOrDefault(p => p.Id == item.Id);
                    if (product is null)
                        throw new InvalidOperationException($"Товар с идентификатором id:{item.Id} не найден в БД!");

                    var order_item = new OrderItem
                    {
                        Order = order,
                        Price = product.Price,
                        Quantity = item.Quantity,
                        Product = product
                    };

                    _db.OrderItems.Add(order_item);
                }
                _db.SaveChanges();
                transaction.Commit();
                return order.ToDTO();
            };
        }
        

        public OrderDTO GetOrderById(int id)=>_db.Orders
            .Include(order => order.OrderItems)
            .FirstOrDefault(order => order.Id == id)
            .ToDTO();
        

        public IEnumerable<OrderDTO> GetUserOrders(string UserName) => _db.Orders
            .Include(order => order.User)
            .Include(order => order.OrderItems)
            .Where(order => order.User.UserName == UserName)
            .ToArray()
            .Select(OrderMapper.ToDTO);
    }
}
