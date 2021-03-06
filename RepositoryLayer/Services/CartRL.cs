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
    public class CartRL : ICartRL
    {
        private SqlConnection sqlConnection;

        public IConfiguration Configuration { get; }

        public CartRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
            sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStoreDB"]);
        }

        public bool AddToCart(CartModel cart)
        {
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sql = new SqlCommand("spAddToCart", sqlConnection);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("UserId", cart.UserId);
                    sql.Parameters.AddWithValue("BookId", cart.BookId);
                    sql.Parameters.AddWithValue("Quantity", cart.Quantity);
                    sqlConnection.Open();
                    var result = Convert.ToInt32(sql.ExecuteNonQuery());
                    sqlConnection.Close();
                    if (result != 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateCart(Cart cart)
        {
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sql = new SqlCommand("spUpdateCart", sqlConnection);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("CartId",cart.CartId);
                    sql.Parameters.AddWithValue("BookId",cart.fkBookId);
                    sql.Parameters.AddWithValue("UserId",cart.fkUserId);
                    sql.Parameters.AddWithValue("Quantity",cart.Quantity);
                    sqlConnection.Open();
                    SqlDataReader reader = sql.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Cart cartwithbooks = new Cart();
                            Book bookincart = new Book();
                            cartwithbooks.CartId = Convert.ToInt32(reader["CartId"]);
                            bookincart.BookId = Convert.ToInt32(reader["fkBookId"]);
                            if (cartwithbooks.CartId == cart.CartId)
                            {
                                var result = sql.ExecuteScalar();
                                if (result != null)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                    //var  result = Convert.ToInt32(sql.e)
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool RemoveFromCart(Cart cart)
        {
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sql = new SqlCommand("spRemoveFromCart", sqlConnection);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("CartId", cart.CartId);
                    sql.Parameters.AddWithValue("BookId", cart.fkBookId);
                    sql.Parameters.AddWithValue("UserId", cart.fkUserId);
                    sql.Parameters.AddWithValue("Quantity", cart.Quantity);
                    sqlConnection.Open();
                    SqlDataReader reader = sql.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Cart cartwithbooks = new Cart();
                            Book bookincart = new Book();
                            cartwithbooks.CartId = Convert.ToInt32(reader["CartId"]);
                            bookincart.BookId = Convert.ToInt32(reader["fkBookId"]);
                            if (cartwithbooks.CartId == cart.CartId)
                            {
                                var result = sql.ExecuteScalar();
                                if (result != null)
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                return false;
                            }
                        }
                        return false;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Cart> GetCartItems(long userid)
        {
            try
            {
                using (sqlConnection)
                {
                    List<Cart> carts = new List<Cart>();
                    SqlCommand sql = new SqlCommand("spGetAllCarts", sqlConnection);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("Userid", userid);
                    sqlConnection.Open();
                    SqlDataReader reader = sql.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Cart cart = new Cart();
                            cart.CartId = Convert.ToInt32(reader["CartId"]);
                            cart.fkBookId = Convert.ToInt32(reader["fkBookId"]);
                            cart.fkUserId = Convert.ToInt32(reader["fkUserId"]);
                            cart.Quantity = Convert.ToInt32(reader["Quantity"]);
                            carts.Add(cart);
                        }
                        return carts;
                    }
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
