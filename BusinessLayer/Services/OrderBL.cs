using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class OrderBL : IOrderBL
    {

        private readonly IOrderRL orderRL;

        public OrderBL(IOrderRL OrderRL)
        {
            this.orderRL = OrderRL;
        }

        public bool AddOrder(OrderModel orderModel)
        {
            try
            {
                return this.orderRL.AddOrder(orderModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Order> GetPastOrders(long userId)
        {
            try
            {
                return this.orderRL.GetAllOrders(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
