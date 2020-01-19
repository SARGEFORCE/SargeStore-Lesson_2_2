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
        public Order CreateOrder(OrderViewModel OrderModel, CartViewModel CartModel, string UserName)
        {
            var user = _UserManager.FindByNameAsync(UserName).Result;
            using (var transaction = _db.Database.BeginTransaction())
            {
                var order = new Order
                {
                    Name = OrderModel.Name,
                    Address = OrderModel.Address,
                    Phone = OrderModel.Phone,
                    User = user,
                    Date = DateTime.Now
                };

                _db.Orders.Add(order);

                foreach (var (product_model, quantity) in CartModel.Items)
                {
                    var product = _db.Products.FirstOrDefault(p => p.Id == product_model.Id);
                    if (product is null)
                        throw new InvalidOperationException($"Товар с идентификатором id:{product_model.Id} не найден в БД!");

                    var order_item = new OrderItem
                    {
                        Order = order,
                        Price = product.Price,
                        Quantity = quantity,
                        Product = product
                    };

                    _db.OrderItems.Add(order_item);
                }
                _db.SaveChanges();
                transaction.Commit();
                return order;
            }
        }

        public Order GetOrderById(int id) => _db.Orders
            .Include(order => order.OrderItems)
            .FirstOrDefault(order => order.Id == id);

        public IEnumerable<Order> GetUserOrders(string UserName) => _db.Orders
            .Include(order => order.User)
            .Include(order => order.OrderItems)
            .Where(order => order.User.UserName == UserName)
            .ToArray();
    }
}
