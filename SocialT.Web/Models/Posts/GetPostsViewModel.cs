namespace SocialT.Web.Models.Posts
{
    using System;
    using System.Linq.Expressions;

    using SocialT.Models;

    public class GetPostsViewModel
    {
        public static Expression<Func<Post, GetPostsViewModel>> FromPost
        {
            get
            {
                return p => new GetPostsViewModel()
                {
                    Content = p.Content,
                    CreatedAt = p.CreatedAt,
                    UserFromName = p.UserFrom.CompanyName ?? p.UserFrom.FirstName + " " + p.UserFrom.LastName,
                    UserFromId = p.UserFromId
                };
            }
        }

        public string Content { get; set; }

        public string UserFromId { get; set; }

        public string UserFromName { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}