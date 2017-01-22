namespace SocialT.Web.Models.Specialties
{
    using SocialT.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;

    public class GetSpecialtiesViewModel
    {
        public static Expression<Func<Specialty, GetSpecialtiesViewModel>> FromSpecialty
        {
            get
            {
                return s => new GetSpecialtiesViewModel()
                {
                    Id = s.Id,
                    Name = s.Name
                };
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}