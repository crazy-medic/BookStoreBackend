using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace RepositoryLayer.Services
{
    public class WishlistRL : IWishlistRL
    {
        public IConfiguration Configuration { get; }
        private SqlConnection sqlConnection;

        public WishlistRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
            sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStoreDB"]);
        }

        public bool AddToWishlist(WishlistModel wishlistModel)
        {
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spAddWishlist", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("fkBookId", wishlistModel.fkBookId);
                    sqlCommand.Parameters.AddWithValue("fkUserId", wishlistModel.fkUserId);
                    sqlConnection.Open();
                    var result = Convert.ToInt32(sqlCommand.ExecuteNonQuery());
                    if(result!=1)
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

        public bool RemoveFromWishlist(WishlistModel wishlistModel)
        {
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spRemoveWishlist", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("fkBookId", wishlistModel.fkBookId);
                    sqlCommand.Parameters.AddWithValue("fkUserId", wishlistModel.fkUserId);
                    sqlConnection.Open();
                    var result = Convert.ToInt32(sqlCommand.ExecuteNonQuery());
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

        public IEnumerable<WishlistEntity> GetAllWishlistBooks(long userid)
        {
            try
            {
                using (sqlConnection)
                {
                    List<WishlistEntity> wishlists = new List<WishlistEntity>();
                    SqlCommand sql = new SqlCommand("spGetWishlist", sqlConnection);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("fkUserId", userid);
                    sqlConnection.Open();
                    SqlDataReader reader = sql.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            WishlistEntity wishlist = new WishlistEntity();
                            wishlist.WishlistId = Convert.ToInt32(reader[0]);
                            wishlist.fkBookId = Convert.ToInt32(reader[1]);
                            wishlist.fkUserId = Convert.ToInt32(reader[2]);
                            wishlists.Add(wishlist);
                        }
                        sqlConnection.Close();
                        return wishlists;
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
