using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL<User>
    {

        private SqlConnection sqlConnection;
        private IConfiguration config;

        public UserRL(IConfiguration config)
        {
            this.config = config;
        }

        public async bool Register(RegisterModel registerModel)
        {
            try
            {
                sqlConnection = new SqlConnection(this.config.GetConnectionString("BookStoreDB"));
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
