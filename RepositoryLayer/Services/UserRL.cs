using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {

        private SqlConnection sqlConnection;
        public IConfiguration Configuration { get; }

        public UserRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public async Task Register(RegisterModel registerModel)
        {
            //sqlConnection = new SqlConnection(this.configuration.GetConnectionString("ConnectionString:BookStoreDB"));
            this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStoreDB"]);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlcmd = new SqlCommand("spAddUser", sqlConnection);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@FullName", registerModel.FullName);
                    sqlcmd.Parameters.AddWithValue("@EmailId",registerModel.EmailID);
                    sqlcmd.Parameters.AddWithValue("@Password",registerModel.Password);
                    sqlcmd.Parameters.AddWithValue("@Phone",registerModel.Phone);
                    sqlcmd.Parameters.AddWithValue("@CreatedAt",DateTime.Now);
                    sqlcmd.Parameters.AddWithValue("@ModifiedAt", DateTime.Now);

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

        public string Login(LoginModel loginModel)
        {
            this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStoreDB"]);
            try
            {
                User user = new User();

                using (sqlConnection)
                {
                    SqlCommand sqlcmd = new SqlCommand("spLoginUser", sqlConnection);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@EmailId", loginModel.EmailId);
                    sqlcmd.Parameters.AddWithValue("@Password", loginModel.Password);
                    sqlConnection.Open();
                    SqlDataReader reader = sqlcmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            user.EmailId = reader["EmailId"].ToString();
                            user.Password = reader["Password"].ToString();
                        }
                        string token = GenerateToken(loginModel.EmailId);
                        return token;
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
            finally
            {
                sqlConnection.Close();
            }
        }

        private static string GenerateToken(string EmailId)
        {
            if (EmailId == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("EmailId", EmailId),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
