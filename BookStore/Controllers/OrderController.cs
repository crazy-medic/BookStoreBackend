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
    public class OrderController : ControllerBase
    {
        private readonly IOrderBL orderBL;

        public OrderController(IOrderBL OrderBL)
        {
            this.orderBL = OrderBL;
        }

        [HttpPost("Buy")]
        public IActionResult AddOrder(OrderModel orderModel)
        {
            try
            {
                User user = new User();
                user.EmailId = User.FindFirst("EmailId").Value.ToString();
                if (user != null)
                {
                    var result = this.orderBL.AddOrder(orderModel);
                    if (result)
                    {
                        return this.Ok(new { status = 200, isSuccess = true, Message = "Order saved to database" });
                    }
                    else
                    {
                        return this.BadRequest(new { status = 400, isSuccess = false, Message = "Failed to add order to database" });
                    }
                }
                else
                {
                    return this.Unauthorized(new { status = 400, isSuccess = false, Message = "Unauthorized" });
                }

            }
            catch (Exception e)
            {
                return this.BadRequest(new { status = 400, isSuccess = false, Message = e.Message });
            }
        }

        [HttpGet("Orders")]
        public IActionResult GetPastOrders()
        {
            try
            {
                User user = new User();
                user.EmailId = User.FindFirst("EmailId").Value.ToString();
                if (user != null)
                {
                    var result = this.orderBL.GetPastOrders(user.UserId);
                    if (result!=null)
                    {
                        return this.Ok(new { status = 200, isSuccess = true, Message = "orders retrieved",data=result });
                    }
                    else
                    {
                        return this.BadRequest(new { status = 400, isSuccess = false, Message = "Failed to get orders" });
                    }
                }
                else
                {
                    return this.Unauthorized(new { status = 400, isSuccess = false, Message = "Unauthorized" });
                }

            }
            catch (Exception e)
            {
                return this.BadRequest(new { status = 400, isSuccess = false, Message = e.Message });
            }
        }
    }
}
