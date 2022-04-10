using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IBookBL
    {
        public bool AddBook(Book book);
        public bool UpdateBook(Book book);
    }
}
