namespace SocialT.Web.Models.Users
{
    using SocialT.Web.App_Start;
    using SocialT.Models;
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public class ActivationUserViewModel
    {
        public static Expression<Func<ApplicationUser, ActivationUserViewModel>> FromApplicationUser
        {
            get
            {
                return u => new ActivationUserViewModel()
                {
                    userId = u.Id,
                    Email = u.Email,
                    RoleId = u.Roles.FirstOrDefault().RoleId,
                    IsActive = u.IsActive
                };
            }
        }

        public string userId { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public string RoleId { get; set; }

        public bool IsActive { get; set; }
    }
}