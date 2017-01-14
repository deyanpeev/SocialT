namespace SocialT.Web.Controllers
{
    using Microsoft.AspNet.Identity;
    using SocialT.Data;
    using SocialT.Web.Models.Posts;
    using System;
    using System.Linq;
    using System.Web.Http;
    using SocialT.Common.Constants;
    using TripExchange.Common.Constants;
    using System.Web.Security;
    using SocialT.Models;

    public class PostsController : BaseApiController
    {
        public PostsController()
            : this(new SocialTData())
        {
        }

        public PostsController(ISocialTData data)
            : base(data)
        {
        }

        [HttpGet]
        [Authorize(Roles = RoleConstants.Student)]
        public IHttpActionResult Get()
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.All().FirstOrDefault(x => x.Id == currentUserId);

            var result = this.Data.Posts.All()
                .Where(p => p.GroupId == currentUser.GroupId)
                .OrderBy(p => p.Id)
                .Select(GetPostsViewModel.FromPost);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Employer)]
        [Authorize(Roles = RoleConstants.Student)]
        public IHttpActionResult CreateNewPost(CreatePostViewModel inputPost)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(GeneralConstants.InvalidRequest);
            }

            if (inputPost.Specialty == null && Roles.IsUserInRole(RoleConstants.Employer))
            {
                return BadRequest("Specialty must be pointed.");
            }

            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.All().FirstOrDefault(x => x.Id == currentUserId);

            Post newPost = new Post()
            {
                Content = inputPost.Content,
                CreatedAt = DateTime.Now,
                GroupId = inputPost.Specialty == null ? currentUser.GroupId : null,
                Specialty = this.Data.Specialties.All().SingleOrDefault(s => s.Name.Equals(inputPost.Specialty)),
                UserFromId = currentUserId
            };

            return Ok(newPost.Id);
        }
    }
}