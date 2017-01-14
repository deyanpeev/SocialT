namespace  SocialT.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using SocialT.Models;

    public class SocialTData : ISocialTData
    {
        private readonly DbContext context;

        private readonly IDictionary<Type, object> repositories;

        public SocialTData()
            : this(new ApplicationDbContext())
        {
        }

        public SocialTData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<ApplicationUser> Users
        {
            get
            {
                return this.GetRepository<ApplicationUser>();
            }
        }

        public IRepository<Skill> Skills
        {
            get
            {
                return this.GetRepository<Skill>();
            }
        }

        //TODO Remove
        public IRepository<Trip> Trips
        {
            get
            {
                return this.GetRepository<Trip>();
            }
        }

        public IRepository<City> Cities
        {
            get
            {
                return this.GetRepository<City>();
            }
        }

        public IRepository<VirtualMachine> VirtualMachines
        {
            get
            {
                return this.GetRepository<VirtualMachine>();
            }
        }

        public IRepository<Message> Messages
        {
            get
            {
                return this.GetRepository<Message>();
            }
        }

        public IRepository<Post> Posts
        {
            get
            {
                return this.GetRepository<Post>();
            }
        }

        public IRepository<Group> Groups
        {
            get
            {
                return this.GetRepository<Group>();
            }
        }

        public IRepository<Specialty> Specialties
        {
            get
            {
                return this.GetRepository<Specialty>();
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(EfRepository<T>);

                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }
    }
}
