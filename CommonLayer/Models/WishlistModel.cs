using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CommonLayer.Models
{
    public class WishlistModel
    {
        public long fkBookId { get; set; }
        public long fkUserId { get; set; }
    }
}
