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
        public IActionResult AddBook(BookModel bookModel)
        {
            try
            {
                User user = new User();
                string email = User.FindFirst("EmailId").Value.ToString();
                if (user.EmailId != null)
                {
                    var result = this.bookBL.AddBook(bookModel);
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
                    return this.Unauthorized(new { status = 400, isSuccess = false, Message = "Unauthorized" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { status = 404, isSuccess = false, Message = e.Message });
            }
        }

        [Authorize]
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
                    return this.Unauthorized(new { status = 400, isSuccess = false, Message = "Unauthorized" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { status = 404, isSuccess = false, Message = e.Message });
            }
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var listofbooks = this.bookBL.GetAllBooks();
                if (listofbooks != null)
                {
                    return this.Ok(new { status = 200, isSuccess = true, Message = "List of all books retrieved", data = listofbooks });
                }
                else
                {
                    return this.NotFound(new { status = 404, isSuccess = false, Message = "No books found in database" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { status = 404, isSuccess = false, Message = e.Message });
            }
        }

        [HttpGet("{id}/GetBook")]
        public IActionResult GetBookbyId(int BookId)
        {
            try
            {
                var bookbyid = this.bookBL.GetBookbyId(BookId);
                if (bookbyid != null)
                {
                    return this.Ok(new { status = 200, isSuccess = true, Message = "List of all books retrieved", data = bookbyid });
                }
                else
                {
                    return this.NotFound(new { status = 404, isSuccess = false, Message = "No books found in database" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { status = 404, isSuccess = false, Message = e.Message });
            }
        }

        [Authorize]
        [HttpDelete("Remove")]
        public IActionResult RemoveBookFromInventory(int BookId)
        {
            try
            {
                User user = new User();
                user.EmailId = User.FindFirst("EmailId").ToString();
                if (user.fkAccountType != 1)
                {
                    var bookbyid = this.bookBL.RemoveBookFromInventory(BookId);
                    if (bookbyid)
                    {
                        return this.Ok(new { status = 200, isSuccess = true, Message = "List of all books retrieved", data = bookbyid });
                    }
                    else
                    {
                        return this.NotFound(new { status = 404, isSuccess = false, Message = "No books found in database" });
                    }
                }
                else
                {
                    return this.Unauthorized(new { status = 401, isSuccess = false, Message = "You are not a seller to remove from inventory" });
                }
                
            }
            catch (Exception e)
            {
                return this.BadRequest(new { status = 404, isSuccess = false, Message = e.Message });
            }
        }
    }
}
