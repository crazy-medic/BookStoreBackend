using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {

        private SqlConnection sqlConnection;
        public IConfiguration config { get; }

        public UserRL(IConfiguration config)
        {
            this.config = config;
        }

        public async Task Register(RegisterModel registerModel)
        {
            try
            {
                sqlConnection = new SqlConnection(this.config.GetConnectionString("BookStoreDB"));
                using (sqlConnection)
                {
                    SqlCommand sqlcmd = new SqlCommand("spAddUser", sqlConnection);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@FullName", registerModel.FullName);
                    sqlcmd.Parameters.AddWithValue("@EmailId",registerModel.EmailID);
                    sqlcmd.Parameters.AddWithValue("@Password",registerModel.Password);
                    sqlcmd.Parameters.AddWithValue("@Phone",registerModel.Phone);
                    sqlcmd.Parameters.AddWithValue("@CreatedAt",DateTime.Now);

                    sqlConnection.Open();
                    sqlcmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
