namespace SocialT.Models
{
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.ComponentModel.DataAnnotations;

    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        //TODO Remove
        private ICollection<Trip> trips;
        private ICollection<Trip> tripsWhereDriver;

        private ICollection<string> interests;
        private ICollection<Skill> skills;
        private ICollection<string> strongAreas;
        private ICollection<string> subjects;
        private ICollection<Message> posts;

        public ApplicationUser()
        {
            this.interests = new HashSet<string>();
            this.skills = new HashSet<Skill>();
            this.strongAreas = new HashSet<string>();
            this.subjects = new HashSet<string>();
            this.posts = new HashSet<Message>();
        }

        [StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; }

        [StringLength(100, MinimumLength = 2)]
        public string LastName { get; set; }

        [Range(2, 6)]
        public double? Grade { get; set; }

        [Range(1, 4)]
        public int? Course { get; set; }

        [StringLength(9)]
        public string FacultyNumber { get; set; }

        public ICollection<string> Interests
        {
            get { return this.interests; }
            set { this.interests = value; }
        }

        public virtual ICollection<Skill> Skills
        {
            get { return this.skills; }
            set { this.skills = value; }
        }

        public virtual ICollection<string> StrongAreas
        {
            get { return this.strongAreas; }
            set { this.strongAreas = value; }
        }

        //Teacher
        public virtual ICollection<string> Subjects
        {
            get { return this.subjects; }
            set { this.subjects = value; }
        }

        public virtual ICollection<Message> Posts
        {
            get { return this.posts; }
            set { this.posts = value; }
        }

        public bool? IsActive { get; set; }

        public virtual int? GroupId { get; set; }

        public virtual Group Gruop { get; set; }

        //Employer
        public string Description { get; set; }

        public string CompanyName { get; set; }

        public string CompanyMoto { get; set; }

        //TODO Remove
        public bool IsDriver { get; set; }

        public string Car { get; set; }

        public virtual ICollection<Trip> Trips
        {
            get { return this.trips; }
            set { this.trips = value; }
        }

        public virtual ICollection<Trip> TripsWhereDriver
        {
            get { return this.tripsWhereDriver; }
            set { this.tripsWhereDriver = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);

            // Add custom user claims here
            return userIdentity;
        }
    }
}