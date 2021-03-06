﻿namespace  SocialT.Data
{
    using SocialT.Models;

    public interface ISocialTData
    {
        IRepository<ApplicationUser> Users { get; }

        IRepository<ApplicationRole> Roles { get; }

        IRepository<Message> Messages { get; }

        IRepository<Group> Groups { get; }

        IRepository<Specialty> Specialties { get; }

        IRepository<Skill> Skills { get; }

        //TODO remove
        IRepository<Trip> Trips { get; }

        IRepository<City> Cities { get; }

        IRepository<VirtualMachine> VirtualMachines { get; }

        IRepository<Post> Posts { get; }

        IRepository<News> News { get; }

        int SaveChanges();
    }
}
