using DataAccessLayer.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SargeStoreDomain.ViewModels;
using SargeStoreDomain.Entities;
using SargeStoreDomain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using SargeStore.Interfaces.Services;
using SargeStoreDomain.DTO.Orders;

namespace SargeStore.Services.FProduct
{
    public class SqlOrderService : IOrderService
    {
        private readonly SargeStoreDB _db;
        private readonly UserManager<User> _UserManager;

        public SqlOrderService(SargeStoreDB db, UserManager<User> UserManager)
        {
            _db = db;
            _UserManager = UserManager;
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
                return new OrderDTO
                {
                    Phone = order.Phone,
                    Address = order.Address,
                    Date = order.Date,
                    OrderItems = order.OrderItems.Select(item => new OrderItemDTO
                    {
                        Id = item.Id,
                        Price = item.Price,
                        Quantity = item.Quantity
                    })
                };
            };
        }
        

        public OrderDTO GetOrderById(int id)
        {var o = _db.Orders
            .Include(order => order.OrderItems)
            .FirstOrDefault(order => order.Id == id);
            return o is null? null : new OrderDTO
            {
                Phone = o.Phone,
                Address = o.Address,
                Date = o.Date,
                OrderItems = o.OrderItems.Select(item => new OrderItemDTO
                {
                    Id = item.Id,
                    Price = item.Price,
                    Quantity = item.Quantity
                })
            };
        }

        public IEnumerable<OrderDTO> GetUserOrders(string UserName) => _db.Orders
            .Include(order => order.User)
            .Include(order => order.OrderItems)
            .Where(order => order.User.UserName == UserName)
            .ToArray()
            .Select( o=> new OrderDTO
            { 
                Phone = o.Phone,
                Address = o.Address,
                Date = o.Date,
                OrderItems = o.OrderItems.Select(item => new OrderItemDTO
                {
                    Id = item.Id,
                    Price = item.Price,
                    Quantity = item.Quantity
                })
            });
    }
}
