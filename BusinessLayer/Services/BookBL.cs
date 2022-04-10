using BusinessLayer.Interfaces;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class BookBL : IBookBL
    {
        private readonly IBookRL bookRL;
        public bool AddBook(Book book)
        {
            try
            {
                return this.bookRL.AddBook(book);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Book> GetAllBooks()
        {
            try
            {
                return this.bookRL.GetAllBooks();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Book> GetBookbyId(int BookId)
        {
            try
            {
                return this.bookRL.GetBookbyId(BookId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateBook(Book book)
        {
            try
            {
                return this.bookRL.UpdateBook(book);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
