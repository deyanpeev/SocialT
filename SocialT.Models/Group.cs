namespace SocialT.Models
{
    using System.Collections.Generic;

    public class Group
    {
        private ICollection<ApplicationUser> students;

        public Group()
        {
            this.students = new HashSet<ApplicationUser>();
        }

        public int Id { get; set; }

        public string Name { get; set; }
        
        public virtual int SpecialtyId { get; set; }

        public virtual Specialty Specialty { get; set; }

        public virtual ICollection<ApplicationUser> Students
        {
            get { return this.students; }
            set { this.students = value; }
        }
    }
}
