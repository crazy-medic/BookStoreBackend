using BusinessLayer.Interfaces;
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
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookBL bookBL;

        public BookController(IBookBL bookBL)
        {
            this.bookBL = bookBL;
        }

        [Authorize]
        [HttpPost("Add")]
        public IActionResult AddBook(Book book)
        {
            try
            {
                User user = new User();
                user.EmailId = User.FindFirst("EmailId").Value.ToString();
                if (user.EmailId != null)
                {
                    var result = this.bookBL.AddBook(book);
                    if (result)
                    {
                        return this.Ok(new { status = 200, isSuccess = true, Message = "Book added to inventory" });
                    }
                    else
                    {
                        return this.BadRequest(new { status = 400, isSuccess = false, Message = "Failed to add book" });
                    }
                }
                else
                {
                    return this.BadRequest(new { status = 400, isSuccess = false, Message = "Unauthorized" });
                }

            }
            catch (Exception e)
            {
                return this.NotFound(new { status = 404, isSuccess = false, Message = e.Message });
            }
        }

        [HttpPost("Update")]
        public IActionResult UpdateBook(Book book)
        {
            try
            {
                User user = new User();
                user.EmailId = User.FindFirst("EmailId").Value.ToString();
                if (user.EmailId != null)
                {
                    var result = this.bookBL.UpdateBook(book);
                    if (result)
                    {
                        return this.Ok(new { status = 200, isSuccess = true, Message = "Book updated in inventory" });
                    }
                    else
                    {
                        return this.BadRequest(new { status = 400, isSuccess = false, Message = "Failed to update book" });
                    }
                }
                else
                {
                    return this.BadRequest(new { status = 400, isSuccess = false, Message = "Unauthorized" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { status = 404, isSuccess = false, Message = e.Message });
            }
        }
    }
}
