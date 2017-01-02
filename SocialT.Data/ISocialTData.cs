namespace  SocialT.Data
{
    using SocialT.Models;

    public interface ISocialTData
    {
        IRepository<ApplicationUser> Users { get; }

        IRepository<Trip> Trips { get; }

        IRepository<City> Cities { get; }

        IRepository<VirtualMachine> VirtualMachines { get; }

        int SaveChanges();
    }
}
