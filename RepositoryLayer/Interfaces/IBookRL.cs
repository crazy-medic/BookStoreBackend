using CommonLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IBookRL
    {
        public bool AddBook(BookModel bookmodel);
        public bool UpdateBook(Book book);
        public IEnumerable<Book> GetAllBooks();
        public IEnumerable<Book> GetBookbyId(int BookId);
        public bool RemoveBookFromInventory(int bookId);
    }
}
