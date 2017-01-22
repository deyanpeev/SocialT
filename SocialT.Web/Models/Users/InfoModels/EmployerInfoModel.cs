namespace SocialT.Web.Models.Users.InfoModels
{
    using SocialT.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public class EmployerInfoModel : IUserInfoModel
    {
        public static Expression<Func<ApplicationUser, EmployerInfoModel>> FromAppUser
        {
            get
            {
                return e => new EmployerInfoModel()
                {
                    Id = e.Id,
                    Email = e.Email,
                    Description = e.Description,
                    CompanyMoto = e.CompanyMoto,
                    CompanyName = e.CompanyName
                };
            }
        }

        public string Description { get; set; }

        public string CompanyName { get; set; }

        public string CompanyMoto { get; set; }
    }
}