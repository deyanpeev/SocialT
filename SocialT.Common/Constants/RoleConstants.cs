namespace SocialT.Common.Constants
{
    public class RoleConstants
    {
        public const string Admin = "Admin";
        public const string Teacher = "Teacher";
        public const string Employer = "Employer";
        public const string Student = "Student";

        public string[] AllRoles
        {
            get
            {
                return new string[] { Admin, Teacher, Student, Employer };
            }
        }
    }
}