namespace SocialT.Web.Controllers
{
    using System.Linq;
    using SocialT.Data;
    using System.Collections.Generic;
    using System.Web.Http;
    using SocialT.Web.Models.Groups;
    using Common.Constants;
    using SocialT.Models;
    using Microsoft.AspNet.Identity;

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
        public IHttpActionResult Get()
        {
            var result = this.Data.Groups.All().Select(GetGroupViewModel.FromGroup);
            return Ok(result);
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult Get(string specialtyName)
        {
            int specialtyId = this.Data.Specialties.All().Single(s => s.Name == specialtyName).Id;
            IQueryable<GetGroupViewModel> result = this.Data.Groups.All().Select(GetGroupViewModel.FromGroup)
                .Where(g => g.SpecialtyId == specialtyId);
            return Ok(result);
        }

        [HttpGet]
        [Authorize(Roles =RoleConstants.Student)]
        [Route("api/Groups/GetGroupByCurrentUser")]
        public IHttpActionResult GetGroupByCurrentUser()
        {
            var currentUserId = User.Identity.GetUserId();
            int currentUserGroupId = (int)this.Data.Users.All().Single(x => x.Id == currentUserId).GroupId;

            var result = this.Data.Groups.All().Select(GetGroupViewModel.FromGroup).Single(g => g.Id == currentUserGroupId);
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