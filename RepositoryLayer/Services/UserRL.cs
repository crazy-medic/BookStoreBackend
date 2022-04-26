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
        IConfiguration Configuration;

        public UserRL(IConfiguration configuration)
        {
            Configuration = configuration;
            sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStoreDB"]);
        }

        public async Task Register(RegisterModel registerModel, int usertype)
        {
            string encryptedPass = Encryptpass(registerModel.Password);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlcmd = new SqlCommand("spAddUser", sqlConnection);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@FullName", registerModel.FullName);
                    sqlcmd.Parameters.AddWithValue("@EmailId",registerModel.EmailID);
                    sqlcmd.Parameters.AddWithValue("@Password",encryptedPass);
                    sqlcmd.Parameters.AddWithValue("@Phone",registerModel.Phone);
                    sqlcmd.Parameters.AddWithValue("@CreatedAt",DateTime.Now);
                    sqlcmd.Parameters.AddWithValue("@UserType", usertype);
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
            try
            {
                User user = new User();
                using (sqlConnection)
                {
                    SqlCommand sqlcmd = new SqlCommand("spLoginUser", sqlConnection);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@EmailId", loginModel.EmailId);
                    sqlConnection.Open();
                    SqlDataReader reader = sqlcmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            user.UserId = Convert.ToInt32(reader["UserId"]);
                            user.EmailId = reader["EmailId"].ToString();
                            user.Password = reader["Password"].ToString();
                            string encryptPass = Encryptpass(loginModel.Password);
                            if (user.Password == encryptPass)
                            {
                                string token = GenerateToken(loginModel.EmailId, user.UserId);
                                sqlConnection.Close();
                                return token;
                            }
                            else
                            {
                                return "Password does not match";
                            }
                        }
                        return null;
                    }
                    else
                    {
                        return "User not found";
                    }
                }
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static string GenerateToken(string EmailId,long Id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes("qrwlrjgnvw;rtivw;tiu");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("EmailId", EmailId),
                    new Claim("Id",Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(12),
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
            try
            {
                using (sqlConnection)
                {
                    User user = new User();
                    SqlCommand sqlcmd = new SqlCommand("spForgetPassword", sqlConnection);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@EmailId", email);
                    sqlConnection.Open();
                    SqlDataReader reader = sqlcmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        var result = sqlcmd.ExecuteNonQuery();
                        if (result != 0)
                        {
                            while (reader.Read())
                            {
                                user.UserId = Convert.ToInt32(reader["UserId"]);
                                user.EmailId = reader["EmailId"].ToString();
                                user.Password = reader["Password"].ToString();
                            }
                            string token = GenerateToken(email,user.UserId);
                            Sender(token);
                            sqlConnection.Close();
                            return token;
                        }
                        else
                        {
                            sqlConnection.Close();
                            return null;
                        }
                    }
                    else
                    {
                        return "No user found for this email";
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
                string link = string.Format("<a href=\"https://localhost:4200/api/User/ResetPassword.aspx?token=" + token + ">Reset Password Link</a>");
                string bodymessage = @"<p>Your password has been reset.Please click the link to create new password.</p>" + link;
                message.Subject = "Reset password link";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = bodymessage;
                smtp.Send(message);
            }
            catch (Exception) { }

            //For a msmq reciver
            msmq.BeginReceive();
        }

        public bool ResetPassword(string email, string password)
        {
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlcmd = new SqlCommand("spResetPassword", sqlConnection);
                    sqlcmd.CommandType = CommandType.StoredProcedure;
                    sqlcmd.Parameters.AddWithValue("@EmailId", email);
                    sqlcmd.Parameters.AddWithValue("@Password", password);
                    sqlConnection.Open();
                    var result = sqlcmd.ExecuteNonQuery();
                    if (result != 0)
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

        public string Encryptpass(string password)
        {
            string msg = "";
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            msg = Convert.ToBase64String(encode);
            return msg;
        }
    }
}
