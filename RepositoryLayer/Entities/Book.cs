using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Entities
{
    public class Book
    {
        public long BookId { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string BookInfo { get; set; }
        public int Quantity { get; set; }
        public float DiscountPrice { get; set; }
        public float ActualPrice { get; set; }
        public string BookImage { get; set; }
        public float Rating { get; set; }
        public long ReviewerCount { get; set; }
    }
}
