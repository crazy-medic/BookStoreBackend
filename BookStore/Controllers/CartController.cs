using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartBL cartBL;

        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }

        [HttpPost("AddToCart")]
        public IActionResult AddToCart(CartModel cart)
        {
            try
            {
                var emailId = User.FindFirst("EmailId").ToString();
                if (emailId != null)
                {
                    var result = this.cartBL.AddToCart(cart);
                    if (result)
                    {
                        return this.Ok(new { status = 200, isSuccess = true, Message = "Book added to cart" });
                    }
                    else
                    {
                        return this.BadRequest(new { status = 400, isSuccess = false, Message = "Failed to add to cart" });
                    }
                }else
                {
                    return this.Unauthorized(new { status = 401, isSuccess = false, Message = "Please log in" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { status = 400, isSuccess = false, Message = e.Message });
            }
        }

        [HttpPut("{id}/update")]
        public IActionResult UpdateCart(Cart cart)
        {
            try
            {
                var emailId = User.FindFirst("EmailId").ToString();
                if (emailId != null)
                {
                    var result = this.cartBL.UpdateCart(cart);
                    if (result)
                    {
                        return this.Ok(new { status = 200, isSuccess = true, Message = "Book in cart updated" });
                    }
                    else
                    {
                        return this.BadRequest(new { status = 400, isSuccess = false, Message = "Failed to update cart" });
                    }
                }
                else
                {
                    return this.Unauthorized(new { status = 401, isSuccess = false, Message = "Please log in" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { status = 400, isSuccess = false, Message = e.Message });
            }
        }
    }
}
