using Microsoft.Extensions.Configuration;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class BookRL : IBookRL
    {
        private SqlConnection sqlConnection;
        private IConfiguration Configuration { get; }
        public BookRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public bool AddBook(Book book)
        {
            sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStoreDB"]);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spAddBook", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("BookName",book.BookName);
                    sqlCommand.Parameters.AddWithValue("Author",book.Author);
                    sqlCommand.Parameters.AddWithValue("BookInfo",book.BookInfo);
                    sqlCommand.Parameters.AddWithValue("Quantity",book.Quantity);
                    sqlCommand.Parameters.AddWithValue("DiscountPrice",book.DiscountPrice);
                    sqlCommand.Parameters.AddWithValue("ActualPrice",book.ActualPrice);
                    sqlCommand.Parameters.AddWithValue("BookImage",book.BookImage);
                    sqlCommand.Parameters.AddWithValue("Rating",book.Rating);
                    sqlCommand.Parameters.AddWithValue("ReviewerCount",book.ReviewerCount);
                    sqlConnection.Open();
                    var result = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    if (result != 1)
                    {
                        sqlConnection.Close();
                        return true;
                    }
                    else
                    {
                        sqlConnection.Close();
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateBook(Book book)
        {
            sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStoreDB"]);
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("spUpdateBook", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("BookId", book.BookId);
                    sqlCommand.Parameters.AddWithValue("BookName", book.BookName);
                    sqlCommand.Parameters.AddWithValue("Author", book.Author);
                    sqlCommand.Parameters.AddWithValue("BookInfo", book.BookInfo);
                    sqlCommand.Parameters.AddWithValue("Quantity", book.Quantity);
                    sqlCommand.Parameters.AddWithValue("DiscountPrice", book.DiscountPrice);
                    sqlCommand.Parameters.AddWithValue("ActualPrice", book.ActualPrice);
                    sqlCommand.Parameters.AddWithValue("BookImage", book.BookImage);
                    sqlCommand.Parameters.AddWithValue("Rating", book.Rating);
                    sqlCommand.Parameters.AddWithValue("ReviewerCount", book.ReviewerCount);
                    sqlConnection.Open();
                    var result = Convert.ToInt32(sqlCommand.ExecuteScalar());
                    if (result != 1)
                    {
                        sqlConnection.Close();
                        return true;
                    }
                    else
                    {
                        sqlConnection.Close();
                        return false;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<Book> GetAllBooks()
        {
            sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStoreDB"]);
            try
            {
                using (sqlConnection)
                {
                    List<Book> books = new List<Book>();
                    SqlCommand sqlCommand = new SqlCommand("spGetAllBooks", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Book book = new Book();
                            book.BookId = Convert.ToInt32(reader["BookId"]);
                            book.BookName = reader["BookName"].ToString();
                            book.Author = reader["Author"].ToString();
                            book.BookInfo = reader["BookInfo"].ToString();
                            book.Quantity = Convert.ToInt32(reader["Quantity"]);
                            book.DiscountPrice = Convert.ToInt32(reader["DiscountPrice"]);
                            book.ActualPrice = Convert.ToInt32(reader["ActualPrice"]);
                            book.BookImage = reader["BookImage"].ToString();
                            book.Rating = Convert.ToInt32(reader["Rating"]);
                            book.ReviewerCount = Convert.ToInt32(reader["ReviewerCount"]);
                            books.Add(book);
                        }
                        sqlConnection.Close();
                        return books;
                    }
                    else
                    {
                        sqlConnection.Close();
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Book> GetBookbyId(int BookId)
        {
            sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStoreDB"]);
            try
            {
                using (sqlConnection)
                {
                    List<Book> books = new List<Book>();
                    SqlCommand sqlCommand = new SqlCommand("spGetBookbyId", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("BookId", BookId);
                    sqlConnection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Book book = new Book();
                            book.BookId = Convert.ToInt32(reader["BookId"]);
                            book.BookName = reader["BookName"].ToString();
                            book.Author = reader["Author"].ToString();
                            book.BookInfo = reader["BookInfo"].ToString();
                            book.Quantity = Convert.ToInt32(reader["Quantity"]);
                            book.DiscountPrice = Convert.ToInt32(reader["DiscountPrice"]);
                            book.ActualPrice = Convert.ToInt32(reader["ActualPrice"]);
                            book.BookImage = reader["BookImage"].ToString();
                            book.Rating = Convert.ToInt32(reader["Rating"]);
                            book.ReviewerCount = Convert.ToInt32(reader["ReviewerCount"]);
                            books.Add(book);
                        }
                        sqlConnection.Close();
                        return books;
                    }
                    else
                    {
                        sqlConnection.Close();
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool RemoveBookFromInventory(int bookId)
        {
            sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStoreDB"]);
            try
            {
                using (sqlConnection)
                {
                    Book book = new Book();
                    SqlCommand sqlCommand = new SqlCommand("spRemoveBook", sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("BookId", bookId);
                    sqlConnection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        int result = Convert.ToInt32(sqlCommand.ExecuteNonQuery());
                        if (result!=1)
                        {
                            sqlConnection.Close();
                            return true;
                        }
                        else
                        {
                            sqlConnection.Close();
                            return false;
                        }
                    }
                    else
                    {
                        sqlConnection.Close();
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
