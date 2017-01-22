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
                    Content = m.Content,
                    From = m.UserFrom.FirstName != null ? m.UserFrom.FirstName + " " + m.UserFrom.LastName
                        : m.UserFrom.CompanyName,
                    SentAt = m.CreatedAt
                };
            }
        }

        public string Content { get; set; }

        public string From { get; set; }

        public DateTime SentAt { get; set; }
    }
}