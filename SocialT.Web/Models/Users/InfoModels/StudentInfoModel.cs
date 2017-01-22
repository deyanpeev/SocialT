namespace SocialT.Web.Models.Users.InfoModels
{
    using SocialT.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Linq;

    public class StudentInfoModel : IUserInfoModel
    {
        public static Expression<Func<ApplicationUser, StudentInfoModel>> FromAppUser
        {
            get
            {
                return s => new StudentInfoModel()
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Course = (int)s.Course,
                    Email = s.Email,
                    FacultyNumber = s.FacultyNumber,
                    Grade = s.Grade.ToString(),
                    Group = s.Group.Name,
                    Skills = s.Skills,
                    Specialty = s.Group.Specialty.Name
                };
            }
        }

        public string FacultyNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Course { get; set; }

        public string Specialty { get; set; }

        public string Group { get; set; }

        public string Grade { get; set; }

        public IEnumerable<Skill> Skills { get; set; }
    }
}