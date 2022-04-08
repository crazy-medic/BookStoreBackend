using CommonLayer.Models;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {

        private SqlConnection sqlConnection;
        MessageQueue msmq = new MessageQueue();
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
                    sqlConnection.Close();
                }
            }
            catch (Exception)
            {
                throw;
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
                        sqlConnection.Close();
                        return token;
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

        public string SendResetLink(string email)
        {
            this.sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStoreDB"]);
            try
            {
                using (sqlConnection)
                {
                    User user = new User();
                    SqlCommand sqlcmd = new SqlCommand("spForgetPassword", sqlConnection);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    var result = sqlcmd.ExecuteNonQuery();
                    if (result!=0)
                    {
                        string token = GenerateToken(email);
                        Sender(token);
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
        }

        public void Sender(string token)
        {
            msmq.Path = @".\private$\Tokens";
            try
            {
                if (!MessageQueue.Exists(msmq.Path))
                {
                    MessageQueue.Create(msmq.Path);
                }
                msmq.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                msmq.ReceiveCompleted += Msmq_ReceiveCompleted;
                msmq.Send(token);
                msmq.BeginReceive();
                msmq.Close();
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
        }

        private void Msmq_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var msg = msmq.EndReceive(e.AsyncResult);
            string token = msg.Body.ToString();
            //mail sending code smtp 
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("vineethclass250@gmail.com", "dummypassword@class")
                };
                message.From = new MailAddress("vineethclass250@gmail.com");
                message.To.Add(new MailAddress("vineethclass250@gmail.com"));
                string bodymessage = @"<p>Your password has been reset.Please click the link to create new password.</p>" + string.Format("<a href=\"https://localhost:4200/api/User/ResetPassword.aspx?token={0}\">Reset Password Link</a>");
                message.Subject = "Reset password link";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = bodymessage;
                smtp.Send(message);
            }
            catch (Exception) { }

            //For a msmq reciver
            msmq.BeginReceive();
        }
    }
}
