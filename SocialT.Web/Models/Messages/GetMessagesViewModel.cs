namespace SocialT.Web.Models.Messages
{
    using SocialT.Models;
    using System;
    using System.Linq.Expressions;

    public class GetMessagesViewModel
    {
        public static Expression<Func<Message, GetMessagesViewModel>> FromMessage
        {
            get
            {
                return m => new GetMessagesViewModel()
                {
                    Subject = m.Subject,
                    Content = m.Content,
                    FromId = m.UserFromId,
                    //First + Second name; Company name; Email
                    FromName = m.UserFrom.FirstName != null ? m.UserFrom.FirstName + " " + m.UserFrom.LastName
                        : (m.UserFrom.CompanyName != null ? m.UserFrom.CompanyName : m.UserFrom.Email),
                    SentAt = m.CreatedAt
                };
            }
        }

        public string Subject { get; set; }

        public string Content { get; set; }

        public string FromId { get; set; }

        public string FromName { get; set; }

        public DateTime SentAt { get; set; }
    }
}