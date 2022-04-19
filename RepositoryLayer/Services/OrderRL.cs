using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class OrderRL : IOrderRL
    {
        private SqlConnection sqlConnection;
        private IConfiguration Configuration { get; }
        public OrderRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
            sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStoreDB"]);
        }

        public bool AddOrder(OrderModel orderModel)
        {
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sql = new SqlCommand("spAddOrder", sqlConnection);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("fkUserId", orderModel.UserId);
                    sql.Parameters.AddWithValue("fkBookId", orderModel.BookId);
                    sql.Parameters.AddWithValue("Quantity", orderModel.Quantity);
                    sql.Parameters.AddWithValue("fkAddress", orderModel.AddressId);
                    orderModel.OrderDateTime = DateTime.Now;
                    sql.Parameters.AddWithValue("OrderDate",orderModel.OrderDateTime);
                    sql.Parameters.AddWithValue("TotalPrice", orderModel.OrderTotal);
                    sqlConnection.Open();
                    var result = Convert.ToInt32(sql.ExecuteScalar());
                    if (result != 1)
                    {
                        sqlConnection.Close();
                        return true;
                    }
                    else
                    {
                        sqlConnection.Close();
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
