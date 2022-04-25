using CommonLayer.Models;
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
    public class FeedbackRL : IFeedbackRL
    {
        public IConfiguration Configuration { get; }
        private SqlConnection sqlConnection;
        public FeedbackRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
            sqlConnection = new SqlConnection(this.Configuration["ConnectionString:BookStoreDB"]);
        }

        public bool AddFeedback(FeedbackModel feedbackModel)
        {
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sql = new SqlCommand("spAddFeedback", sqlConnection);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("@Rating", feedbackModel.Rating);
                    sql.Parameters.AddWithValue("@Review", feedbackModel.Review);
                    sql.Parameters.AddWithValue("@UserId", feedbackModel.fkUserId);
                    sql.Parameters.AddWithValue("@BookId", feedbackModel.fkBookId);
                    sqlConnection.Open();
                    var result = Convert.ToInt32(sql.ExecuteNonQuery());
                    if (result != 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<FeedbackEntity> GetCommentsAndReviews(long bookid)
        {
            try
            {
                using (sqlConnection)
                {
                    List<FeedbackEntity> feedbacks = new List<FeedbackEntity>();
                    SqlCommand sql = new SqlCommand("spGetBookRatingReviews", sqlConnection);
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("@Bookid", bookid);
                    sqlConnection.Open();
                    SqlDataReader reader = sql.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            FeedbackEntity feedback = new FeedbackEntity();
                            feedback.FeedbackId = Convert.ToInt32(reader[0]);
                            feedback.Rating = (float)reader[1];
                            feedback.Review = reader[2].ToString();
                            feedback.fkBookId = Convert.ToInt32(reader[3]);
                            feedback.fkUserId = Convert.ToInt32(reader[4]);
                            feedbacks.Add(feedback);
                        }
                        sqlConnection.Close();
                        return feedbacks;
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
    }
}
