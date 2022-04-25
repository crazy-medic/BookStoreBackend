using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class FeedbackBL : IFeedbackBL
    {
        IFeedbackRL feedbackRL;
        public FeedbackBL(IFeedbackRL feedbackRL)
        {
            this.feedbackRL = feedbackRL;
        }
        public bool AddFeedback(FeedbackModel feedbackModel)
        {
            try
            {
                return this.feedbackRL.AddFeedback(feedbackModel);
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
                return this.feedbackRL.GetCommentsAndReviews(bookid);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
