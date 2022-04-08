using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        public Task Register(RegisterModel registerModel);

        public string Login(LoginModel loginModel);
        public string SendResetLink(string email);
        public bool ResetPassword(string email, string password);
    }
}
