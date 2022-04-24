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
    public class WishlistController : ControllerBase
    {
        private readonly IWishlistBL wishlistBL;

        public WishlistController(IWishlistBL WishlistBL)
        {
            this.wishlistBL = WishlistBL;
        }
    
        [HttpPost("Add")]
        public IActionResult AddToWishlist(WishlistModel wishlistModel)
        {
            long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
            wishlistModel.fkUserId = userid;
            if (userid > 0)
            {
                try
                {
                    var result = this.wishlistBL.AddToWishlist(wishlistModel);
                    if (result)
                    {
                        return this.Ok(new { status = 200, isSuccess = true, Message = "Book added to wishlist successfully" });
                    }
                    else
                    {
                        return this.BadRequest(new { status = 400, isSuccess = false, Message = "Failed to add book" });
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

        [HttpDelete("Remove")]
        public IActionResult RemoveFromWishlist(WishlistModel wishlistModel)
        {
            long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
            wishlistModel.fkUserId = userid;
            if (userid > 0)
            {
                try
                {
                    var result = this.wishlistBL.RemoveFromWishlist(wishlistModel);
                    if (result)
                    {
                        return this.Ok(new { status = 200, isSuccess = true, Message = "Book removed from wishlist successfully" });
                    }
                    else
                    {
                        return this.BadRequest(new { status = 400, isSuccess = false, Message = "Failed to remove book" });
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

        [HttpGet("Get")]
        public IActionResult GetAllWishlistBooks()
        {
            long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
            if (userid > 0)
            {
                try
                {
                    var result = this.wishlistBL.GetAllWishlistBooks(userid);
                    if (result!=null)
                    {
                        return this.Ok(new { status = 200, isSuccess = true, Message = "Book got from wishlist successfully" });
                    }
                    else
                    {
                        return this.BadRequest(new { status = 400, isSuccess = false, Message = "Failed to get wishlist of books" });
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
    }
}
