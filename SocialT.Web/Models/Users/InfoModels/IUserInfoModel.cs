namespace SocialT.Web.Models.Users.InfoModels
{
    using SocialT.Models;
    using System;
    using System.Linq.Expressions;

    public class IUserInfoModel
    {
        public static Expression<Func<ApplicationUser, IUserInfoModel>> FromAppUser
        {
            get
            {
                return e => new IUserInfoModel()
                {
                    Id = e.Id,
                    Email = e.Email,
                };
            }
        }

        public string Id { get; set; }

        public string Email { get; set; }
        
        public bool IsSameUser { get; set; }

        public string RoleName { get; set; }
    }
}