namespace SocialT.Web.Controllers
{
    using Microsoft.AspNet.Identity;
    using SocialT.Common.Constants;
    using SocialT.Data;
    using SocialT.Models;
    using SocialT.Web.Controllers;
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Http;

    [RoutePrefix("api/Skills")]
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
        [Route("AddSkill")]
        public IHttpActionResult AddSkill(string skillName)
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.All().FirstOrDefault(x => x.Id == currentUserId);

            if(!this.Data.Skills.All().Select(s => s.Name).Contains(skillName))
            {
                this.Data.Skills.Add(new Skill()
                {
                    Name = skillName
                });
            }

            Skill skill = this.Data.Skills.All().Single(s => s.Name == skillName);
            if (!currentUser.Skills.Select(s => s.Name).Contains(skillName))
            {
                currentUser.Skills.Add(skill);
            }
            else
            {
                return BadRequest("This skill is already present.");
            }

            return Ok("Skill successfully added.");   
        }

        [HttpPost]
        [Authorize]
        [Route("EndorseSkill")]
        public IHttpActionResult EndorseSkill()
        {

        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Student)]
        [Route("RemoveSkill")]
        public IHttpActionResult RemoveSkill(string skillName)
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.All().FirstOrDefault(x => x.Id == currentUserId);

            if(!currentUser.Skills.Select(s => s.Name).Contains(skillName))
            {
                return BadRequest("This skill is not present");
            }

            Skill skill = this.Data.Skills.All().Single(s => s.Name == skillName);
            currentUser.Skills.Remove(skill);

            return Ok("Skill successfully removed");
        }
    }
}