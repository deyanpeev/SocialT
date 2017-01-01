namespace SocialUnion.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;

    using SocialUnion.Data;
    using SocialUnion.Web.Models.Stats;

    public class StatsController : BaseApiController
    {
        public StatsController()
            : this(new SocialUnionData())
        {
        }

        public StatsController(ISocialUnionData data)
            : base(data)
        {
        }

        [HttpGet]
        public StatsViewModel Get()
        {
            var stats = new StatsViewModel
                            {
                                Drivers = this.Data.Users.All().Count(user => user.IsDriver),
                                FinishedTrips =
                                    this.Data.Trips.All().Count(trip => trip.DepartureTime > DateTime.Now),
                                Trips = this.Data.Trips.All().Count(),
                                Users = this.Data.Users.All().Count(),
                            };

            return stats;
        }
    }
}