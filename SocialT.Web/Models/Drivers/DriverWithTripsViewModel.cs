namespace  SocialT.Web.Models.Drivers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using SocialT.Models;
    using SocialT.Web.Models.Trips;

    public class DriverWithTripsViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int NumberOfUpcomingTrips { get; set; }

        public int NumberOfTotalTrips { get; set; }

        public IEnumerable<TripInfoViewModel> Trips { get; set; }
    }
}