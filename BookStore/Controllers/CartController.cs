using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Authorize]
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

        [HttpDelete("{id}/RemoveFromCart")]
        public IActionResult RemoveFromCart(Cart cart)
        {
            try
            {
                var emailId = User.FindFirst("EmailId").ToString();
                if (emailId != null)
                {
                    var result = this.cartBL.RemoveFromCart(cart);
                    if (result)
                    {
                        return this.Ok(new { status = 200, isSuccess = true, Message = "Book removed" });
                    }
                    else
                    {
                        return this.BadRequest(new { status = 400, isSuccess = false, Message = "Failed to remove the book" });
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

        [HttpGet("Get")]
        public IActionResult GetCartItems()
        {
            return this.Ok(new { status = 200, isSuccess = true, Message = "got books" });
        }
    }
}
