using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IUserBL
    {
        public Task Register(RegisterModel registerModel,int usertype);
        public string Login(LoginModel loginModel);
        public string SendResetLink(string email);
        public bool ResetPassword(string email, string password);
    }
}
