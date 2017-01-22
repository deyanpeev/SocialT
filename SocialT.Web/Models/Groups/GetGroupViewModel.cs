namespace SocialT.Web.Models.Groups
{
    using SocialT.Models;
    using System;
    using System.Linq.Expressions;

    public class GetGroupViewModel
    {
        public static Expression<Func<Group, GetGroupViewModel>> FromGroup
        {
            get
            {
                return g => new GetGroupViewModel()
                {
                    Id = g.Id,
                    Name = g.Name,
                    SpecialtyId = g.SpecialtyId,
                    SpecialtyName = g.Specialty.Name
                };
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int SpecialtyId { get; set; }

        public string SpecialtyName { get; set; }
    }
}