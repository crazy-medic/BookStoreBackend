using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class CartModel
    {
        public long? UserId { get; set; }
        public long BookId { get; set; }
        public long Quantity { get; set; }
    }
}
