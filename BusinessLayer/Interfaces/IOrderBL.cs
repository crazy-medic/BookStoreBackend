using CommonLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IOrderBL
    {
        public bool AddOrder(OrderModel orderModel);
        public IEnumerable<Order> GetPastOrders(long userId);
    }
}
