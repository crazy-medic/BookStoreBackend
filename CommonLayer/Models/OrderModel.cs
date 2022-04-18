using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class OrderModel
    {
        public long UserId { get; set; }
        public long AddressId { get; set; }
        public long BookId { get; set; }
        public float OrderTotal { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDateTime { get; set; }
    }
}
