using CommonLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IOrderRL
    {
        public bool AddOrder(OrderModel orderModel);
        public IEnumerable<Order> GetAllOrders(long userId);
    }
}
