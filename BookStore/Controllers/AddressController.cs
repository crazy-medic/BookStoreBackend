using BusinessLayer.Interfaces;
using RepositoryLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressBL addressBL;

        public AddressController(IAddressBL addressBL)
        {
            this.addressBL = addressBL;
        }

        [HttpPost("{userid}/Add")]
        public IActionResult AddAddress(AddressEntity address)
        {
            long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
            if (userid>0)
            {
                try
                {
                    var result = this.addressBL.AddAddress(address);
                    if (result)
                    {
                        return this.Ok(new { status = 200, isSuccess = true, Message = "Address added successfully" });
                    }
                    else
                    {
                        return this.BadRequest(new { status = 400, isSuccess = false, Message = "Failed to add address" });
                    }
                }
                catch (Exception e)
                {
                    return this.BadRequest(new { status = 400, isSuccess = false, Message = e.InnerException.Message });
                }
            }
            else
            {
                return this.Unauthorized(new { status = 401, isSuccess = false, Message = "Unauthorized" });
            }
        }

        [HttpPost("{userid}/Update")]
        public IActionResult UpdateAddress(AddressEntity address)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (userid > 0)
                {
                    var result = this.addressBL.UpdateAddress(address);
                    if (result)
                    {
                        return this.Ok(new { status = 200, isSuccess = true, Message = "Address updated successfully" });
                    }
                    else
                    {
                        return this.BadRequest(new { status = 400, isSuccess = false, Message = "Failed to update address" });
                    }
                }
                else
                {
                    return this.Unauthorized(new { status = 401, isSuccess = false, Message = "Unauthorized" });
                }

            }
            catch (Exception e)
            {
                return this.BadRequest(new { status = 400, isSuccess = false, Message = e.InnerException.Message });
            }
        }
    }
}
