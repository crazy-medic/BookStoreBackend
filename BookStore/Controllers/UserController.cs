using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;

        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterModel registerModel)
        {
            try
            {
                await this.userBL.Register(registerModel);
                return this.Ok(new { status = 200, isSuccess = true, Message = "Registered successfully", data = registerModel });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { status = 400, isSuccess = false, Message = e.Message });
            }
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginModel loginModel)
        {
            try
            {
                var login = this.userBL.Login(loginModel);
                return this.Ok(new { status = 200,isSuccess=true,Message="Logged in",data=login });
            }
            catch (Exception e)
            {
                return this.BadRequest(new { status = 400, isSuccess = false, Message = e.Message });
            }
        }

        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            if (email == null)
            {
                return this.BadRequest(new { Status = 400, isSuccess = false, Message = "Enter an email" });
            }
            try
            {
                if (this.userBL.SendResetLink(email) != null)
                {
                    return this.Ok(new { Status = 200, isSuccess = true, Message = "Reset password link sent" });
                }
                else
                {
                    return this.BadRequest(new { Status = 400, isSuccess = false, Message = "Email not found in database" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { Status = 400, isSuccess = false, Message = e.InnerException.Message });
            }
        }

        [HttpPost("ResetPassword")]
        [Authorize]
        public IActionResult ResetPassword(string Password,string ConfirmPassword)
        {
            var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
            if (Password == ConfirmPassword)
            {
                var reset = this.userBL.ResetPassword(email, Password);
                return this.Ok(new { status = 200, isSuccess = true, Message = "Password successfully reset", data = Password });
            }
            else
            {
                return this.BadRequest(new { status = 400, isSuccess = false, Message = "Passwords dont match",data=Password,ConfirmPassword });
            }
        }
    }
}
