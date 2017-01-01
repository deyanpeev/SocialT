namespace  SocialUnion.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    using SocialUnion.Models;

    public class SocialUnionData : ISocialUnionData
    {
        private readonly DbContext context;

        private readonly IDictionary<Type, object> repositories;

        public SocialUnionData()
            : this(new ApplicationDbContext())
        {
        }

        public SocialUnionData(DbContext context)
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

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(EfRepository<T>);

                //// if (typeof(T).IsAssignableFrom(typeof(UserProfile)))
                //// {
                ////     type = typeof(UsersRepository);
                //// }

                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }
    }
}
