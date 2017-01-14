namespace SocialT.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Specialty
    {
        private ICollection<ApplicationUser> students;

        public Specialty()
        {
            this.students = new HashSet<ApplicationUser>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ApplicationUser> Students
        {
            get
            {
                return this.students;
            }
        }
    }
}
