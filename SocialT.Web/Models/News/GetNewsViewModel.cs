namespace SocialT.Web.Models.News
{
    using System;
    using System.Linq.Expressions;
    using SocialT.Models;

    public class GetNewsViewModel
    {
        public static Expression<Func<News, GetNewsViewModel>> FromNews
        {
            get
            {
                return n => new GetNewsViewModel()
                {
                    Subject = n.Subject,
                    Content = n.Content,
                    CreatedAt = n.CreatedAt
                };
            }
        }

        public string Subject { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}