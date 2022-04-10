using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Entities
{
    public class Book
    {
        public long BookId;
        public string BookName;
        public string Author;
        public string BookInfo;
        public int Quantity;
        public float DiscountPrice;
        public float ActualPrice;
        public string BookImage;
        public float Rating;
        public long ReviewerCount;
    }
}
