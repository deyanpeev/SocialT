namespace  SocialT.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using SocialT.Models;
    using SocialT.Data.Migrations;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public IDbSet<Post> Posts { get; set; }

        public IDbSet<Message> Messages { get; set; }

        public IDbSet<Group> Groups { get; set; }

        public IDbSet<Specialty> Specialties { get; set; }

        public IDbSet<Skill> Skills { get; set; }

        //TODO Remove
        public IDbSet<Trip> Trips { get; set; }

        public IDbSet<City> Cities { get; set; }

        public IDbSet<VirtualMachine> VirtualMachines { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Trip>()
                .HasRequired(m => m.Driver)
                .WithMany(m => m.TripsWhereDriver)
                .HasForeignKey(m => m.DriverId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Trip>()
                .HasMany(m => m.Passengers)
                .WithMany(m => m.Trips);

            //TODO Modify
        }
    }
}