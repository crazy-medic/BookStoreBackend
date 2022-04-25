using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackBL feedbackBL;
        public FeedbackController(IFeedbackBL FeedbackBL)
        {
            this.feedbackBL = FeedbackBL;
        }

        [HttpPost("Rating")]
        public IActionResult AddFeedback(float rating,string review,long bookid)
        {
            try
            {
                FeedbackModel feedbackModel = new FeedbackModel();
                long userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "Id").Value);
                if (userid > 0)
                {
                    feedbackModel.fkUserId = userid;
                    feedbackModel.Rating = rating;
                    feedbackModel.Review = review;
                    feedbackModel.fkBookId = bookid;
                    var result = this.feedbackBL.AddFeedback(feedbackModel);
                    if (result)
                    {
                        return this.Ok(new { status = 200, isSuccess = true, Message = "Book rated successfully" });
                    }
                    else
                    {
                        return this.BadRequest(new { status = 400, isSuccess = false, Message = "Failed to add rating" });
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

        [HttpGet("Comments")]
        public IActionResult GetRatingsAndReviews(long bookid)
        {
            try
            {
                var result = this.feedbackBL.GetCommentsAndReviews(bookid);
                if (result != null)
                {
                    return this.Ok(new { status = 200, isSuccess = true, Message = "Book rating and reviews retrieved successfully" });
                }
                else
                {
                    return this.BadRequest(new { status = 400, isSuccess = false, Message = "Failed to get ratings and reviews" });
                }
            }
            catch (Exception e)
            {
                return this.BadRequest(new { status = 400, isSuccess = false, Message = e.InnerException.Message });
            }
        }
    }
}
