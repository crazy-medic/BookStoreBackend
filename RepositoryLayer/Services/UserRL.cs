using CommonLayer.Models;
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

        public UserRL(IConfiguration config)
        {

        }

        public bool Register(RegisterModel registerModel)
        {
            try
            {
                
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
