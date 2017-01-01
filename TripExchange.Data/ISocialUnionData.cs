namespace  SocialUnion.Data
{
    using SocialUnion.Models;

    public interface ISocialUnionData
    {
        IRepository<ApplicationUser> Users { get; }

        IRepository<Trip> Trips { get; }

        IRepository<City> Cities { get; }

        IRepository<VirtualMachine> VirtualMachines { get; }

        int SaveChanges();
    }
}
