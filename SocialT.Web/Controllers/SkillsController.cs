namespace TripExchange.Web.Controllers
{
    using Microsoft.AspNet.Identity;
    using SocialT.Common.Constants;
    using SocialT.Data;
    using SocialT.Web.Controllers;
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Http;

    public class SkillsController : BaseApiController
    {
        public SkillsController()
            : this(new SocialTData())
        {
        }

        public SkillsController(ISocialTData data)
            : base(data)
        {
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Student)]
        [Route("api/Skills/AddSkill")]
        public IHttpActionResult AddSkill(string skillName)
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.All().FirstOrDefault(x => x.Id == currentUserId);

            return Ok();   
        }
    }
}