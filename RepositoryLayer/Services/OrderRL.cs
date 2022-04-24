using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Entities;
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

        public IEnumerable<Order> GetAllOrders(long userId)
        {
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sql = new SqlCommand("spGetAllOrders", sqlConnection);
                    sql.CommandType = CommandType.StoredProcedure;
                    SqlCommand sqlc = new SqlCommand("spGetAddress", sqlConnection);
                    sqlc.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("@UserId", userId);
                    List<Order> orderlist = new List<Order>();
                    List<AddressEntity> addresslist = new List<AddressEntity>();
                    SqlDataReader reader = sql.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Order order = new Order();
                            order.OrderId = Convert.ToInt32(reader["OrderId"]);
                            order.UserId = Convert.ToInt32(reader["fkUserId"]);
                            order.BookId = Convert.ToInt32(reader["fkBookId"]);
                            order.AddressId = Convert.ToInt32(reader["fkAddress"]);
                            order.OrderDateTime = (DateTime)reader["OrderDate"];
                            order.Quantity = Convert.ToInt32(reader["Quantity"]);
                            order.OrderTotal = (float)reader["TotalPrice"];
                            AddressEntity addressEntity = new AddressEntity();
                            sqlc.Parameters.AddWithValue("@AddressId", order.AddressId);
                            reader = sqlc.ExecuteReader();
                            while (reader.Read())
                            {
                                addressEntity.AddressId = Convert.ToInt32(reader[0]);
                                addressEntity.fkUserId = Convert.ToInt32(reader[1]);
                                addressEntity.Address = reader[2].ToString();
                                addressEntity.City = reader[3].ToString();
                                addressEntity.State = reader[4].ToString();
                                addressEntity.PinCode = Convert.ToInt32(reader[5]);
                                addressEntity.fkAddressType = Convert.ToInt32(reader[6]);
                                if(addressEntity.AddressId == order.AddressId)
                                {
                                    addresslist.Add(addressEntity);
                                }
                            }
                            orderlist.Add(order);
                        }
                        sqlConnection.Close();
                        return orderlist;
                    }
                    else
                    {
                        sqlConnection.Close();
                        return null;
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
