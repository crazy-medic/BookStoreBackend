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

        public bool DeleteAddress(AddressEntity address)
        {
            try
            {
                sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStoreDB"]);
                using (sqlConnection)
                {
                    SqlCommand sql = new SqlCommand("spRemoveAddress", sqlConnection);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("Address", address.Address);
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

        public IEnumerable<AddressEntity> GetAllAddress(long userid)
        {
            try
            {
                sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStoreDB"]);
                using (sqlConnection)
                {
                    SqlCommand sql = new SqlCommand("spGetAllAddress", sqlConnection);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("fkUserId", userid);
                    sqlConnection.Open();
                    List<AddressEntity> address = new List<AddressEntity>();
                    SqlDataReader reader = sql.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            AddressEntity addressEntity = new AddressEntity();
                            addressEntity.AddressId = Convert.ToInt32(reader["AddressId"]);
                            addressEntity.fkUserId = Convert.ToInt32(reader["fkUserId"]);
                            addressEntity.Address = reader["Address"].ToString();
                            addressEntity.City = reader["City"].ToString();
                            addressEntity.State = reader["State"].ToString();
                            addressEntity.PinCode = Convert.ToInt32(reader["PinCode"]);
                            addressEntity.fkAddressType = Convert.ToInt32(reader["fkAddressType"]);
                            address.Add(addressEntity);
                        }
                        return address;
                    }
                    else
                    {
                        return null;
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<AddressTypes> GetAddressTypes()
        {
            try
            {
                sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStoreDB"]);
                using (sqlConnection)
                {
                    SqlCommand sql = new SqlCommand("spGetAllAddressTypes", sqlConnection);
                    sql.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    List<AddressTypes> addressType = new List<AddressTypes>();
                    SqlDataReader reader = sql.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            AddressTypes type = new AddressTypes();
                            type.AddressType = reader["AddressType"].ToString();
                            type.TypeId = Convert.ToInt32(reader["TypeId"]);
                            addressType.Add(type);
                        }
                        return addressType;
                    }
                    else
                    {
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
