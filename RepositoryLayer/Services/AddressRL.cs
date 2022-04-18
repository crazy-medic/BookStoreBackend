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
    public class AddressRL : IAddressRL
    {
        public IConfiguration Configuration { get; }
        private SqlConnection sqlConnection;

        public AddressRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public bool AddAddress(AddressEntity address)
        {
            try
            {
                sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStoreDB"]);
                using (sqlConnection)
                {
                    SqlCommand sql = new SqlCommand("spAddAddress", sqlConnection);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("Address",address.Address);
                    sql.Parameters.AddWithValue("fkUserId",address.fkUserId);
                    sql.Parameters.AddWithValue("City",address.City);
                    sql.Parameters.AddWithValue("State",address.State);
                    sql.Parameters.AddWithValue("PinCode",address.PinCode);
                    sql.Parameters.AddWithValue("AddressType",address.fkAddressType);
                    sqlConnection.Open();
                    var result = Convert.ToInt32(sql.ExecuteScalar());
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

        public bool UpdateAddress(AddressEntity address)
        {
            try
            {
                sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStoreDB"]);
                using (sqlConnection)
                {
                    SqlCommand sql = new SqlCommand("spUpdateAddress", sqlConnection);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("AddressId",address.AddressId);
                    sql.Parameters.AddWithValue("Address", address.Address);
                    sql.Parameters.AddWithValue("fkUserId", address.fkUserId);
                    sql.Parameters.AddWithValue("City", address.City);
                    sql.Parameters.AddWithValue("State", address.State);
                    sql.Parameters.AddWithValue("PinCode", address.PinCode);
                    sql.Parameters.AddWithValue("AddressType", address.fkAddressType);
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
