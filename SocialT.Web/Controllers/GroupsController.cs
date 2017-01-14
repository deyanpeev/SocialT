namespace SocialT.Web.Controllers
{
    using System.Linq;
    using SocialT.Data;
    using System.Collections.Generic;
    using System.Web.Http;
    using SocialT.Web.Models.Group;
    using Common.Constants;
    using SocialT.Models;

    public class GroupsController : BaseApiController
    {
        public GroupsController()
            : this(new SocialTData())
        {
        }

        public GroupsController(ISocialTData data)
            : base(data)
        {
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/Groups/GetAllGroups")]
        public IHttpActionResult GetAllGorups()
        {
            var result = this.Data.Groups.All().Select(GetGroupViewModel.FromGroup);
            return Ok(result);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/Groups/GetAllGroupsBySpecialty")]
        public IHttpActionResult GetAllGorupsBySpecialty(int specialtyId)
        {
            GetGroupViewModel result = this.Data.Groups.All().Select(GetGroupViewModel.FromGroup)
                .First(g => g.SpecialtyId == specialtyId);
            return Ok(result);
        }

        [HttpPost]
        [Route("api/Groups/GreateGroup")]
        [Authorize(Roles = RoleConstants.Admin)]
        public IHttpActionResult CreateGroup(string name)
        {
            //does group exist
            if(this.Data.Groups.All().Any(g => g.Name == name))
            {
                return BadRequest("Group with the specified name already exists!");
            }

            var group = new Group()
            {
                Name = name
            };
            this.Data.Groups.Add(group);
            this.Data.SaveChanges();

            return Ok("Group successfully created.");
        }
    }
}