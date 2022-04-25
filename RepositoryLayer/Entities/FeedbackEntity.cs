using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entities
{
    public class FeedbackEntity
    {
        public long FeedbackId { get; set; }
        public float Rating { get; set; }
        public string Review { get; set; }

        [ForeignKey("book")]
        public long fkBookId { get; set; }
        public virtual Book book { get; set; }

        [ForeignKey("user")]
        public long fkUserId { get; set; }
        public virtual User user { get; set; }

    }
}
