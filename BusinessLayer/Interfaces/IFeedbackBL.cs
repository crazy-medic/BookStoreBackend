using CommonLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interfaces
{
    public interface IFeedbackBL
    {
        public bool AddFeedback(FeedbackModel feedbackModel);
        public IEnumerable<FeedbackEntity> GetCommentsAndReviews(long bookid);
    }
}
