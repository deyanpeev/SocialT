namespace SocialT.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Skill
    {
        private ICollection<ApplicationUser> endorements;

        public Skill()
        {
            this.endorements = new HashSet<ApplicationUser>();
        }

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ApplicationUser User { get; set; }
        
        public string UserId { get; set; }

        public virtual ICollection<ApplicationUser> Endorsements
        {
            get { return this.endorements; }
            set { this.endorements = value; }
        }
    }
}
