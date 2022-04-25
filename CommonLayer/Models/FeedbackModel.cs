using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Models
{
    public class FeedbackModel
    {
        public float? Rating { get; set; }
        public string Review { get; set; }
        public long fkBookId { get; set; }
        public long fkUserId { get; set; }
    }
}
