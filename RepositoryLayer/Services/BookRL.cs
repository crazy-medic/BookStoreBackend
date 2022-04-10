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
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                    return true;
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
    }
}
