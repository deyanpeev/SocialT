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
                    //First + Second name; Company name; Email
                    UserFromName = p.UserFrom.FirstName != null ? p.UserFrom.FirstName + " " + p.UserFrom.LastName
                        : (p.UserFrom.CompanyName != null ? p.UserFrom.CompanyName : p.UserFrom.Email),
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