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
    using SocialT.Web.Models.Skills;

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
        public IHttpActionResult AddSkill([FromBody]string skillName)
        {
            if (string.IsNullOrEmpty(skillName))
            {
                return BadRequest("The name cannot be null or empty");
            }

            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.All().FirstOrDefault(x => x.Id == currentUserId);

            var currentUserSkills = this.Data.Skills.All().Where(s => s.UserId == currentUserId);

            Skill skill = this.Data.Skills.All().SingleOrDefault(s => s.Name == skillName);
            if (!currentUserSkills.Select(s => s.Name).Contains(skillName))
            {
                currentUser.Skills.Add(new Skill()
                {
                    Name = skillName,
                    UserId = currentUserId
                });
                this.Data.SaveChanges();
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
        public IHttpActionResult EndorseSkill(EndorseSkillViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(GeneralConstants.InvalidRequest);
            }

            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.All().FirstOrDefault(x => x.Id == currentUserId);

            if(model.UserId == currentUserId)
            {
                return BadRequest("User dannot endorse his own skills.");
            }

            var userToEndorse = this.Data.Users.All().FirstOrDefault(x => x.Id == model.UserId);
            var skill = userToEndorse.Skills.FirstOrDefault(s => s.Id == model.SkillId);
            if (skill == null)
            {
                return BadRequest("User doesn't have such skill"); 
            }

            return Ok("Skill successfully added.");
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Student)]
        [Route("RemoveSkill")]
        public IHttpActionResult RemoveSkill(string skillName)
        {
            if (string.IsNullOrEmpty(skillName))
            {
                return BadRequest("The name cannot be empty.");
            }

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