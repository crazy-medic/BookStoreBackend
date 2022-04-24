using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        IUserRL userRL;

        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public async Task Register(RegisterModel registerModel, int usertype)
        {
            try
            {
                await userRL.Register(registerModel,usertype);
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
                return userRL.Login(loginModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string SendResetLink(string email)
        {
            try
            {
                return this.userRL.SendResetLink(email);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ResetPassword(string email,string password)
        {
            try
            {
                return this.userRL.ResetPassword(email, password);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
