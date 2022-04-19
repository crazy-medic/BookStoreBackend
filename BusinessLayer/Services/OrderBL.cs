using BusinessLayer.Interfaces;
using CommonLayer.Models;
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
    }
}
