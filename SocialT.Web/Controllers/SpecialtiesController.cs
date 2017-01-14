namespace SocialT.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    using SocialT.Data;

    public class SpecialtiesController : BaseApiController
    {
        public SpecialtiesController()
            : this(new SocialTData())
        {
        }

        public SpecialtiesController(ISocialTData data)
            : base(data)
        {
        }

        /// <summary>
        /// Return list of all cities sorted by name
        /// </summary>
        /// <returns>List of strings with all cities sorted by name</returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            var specialties = this.Data.Specialties.All().Select(s => s.Name).ToList();
            specialties.Sort();
            return specialties;
        }
    }
}