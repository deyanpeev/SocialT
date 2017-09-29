namespace SocialT.Web.Controllers
{
    using Microsoft.AspNet.Identity;
    using SocialT.Data;
    using SocialT.Web.Models.Posts;
    using System;
    using System.Linq;
    using System.Web.Http;
    using SocialT.Common.Constants;
    using System.Web.Security;
    using SocialT.Models;
    using System.Linq.Expressions;

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
        [Route("api/Posts/GetAllPostsForUserGroup")]
        public IHttpActionResult GetAllPostsForUserGroup()
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.GetById(currentUserId);

            return this.GetAllPostsFor(p => p.GroupId == currentUser.GroupId);
        }

        [HttpGet]
        [Authorize(Roles = RoleConstants.Student)]
        [Route("api/Posts/GetAllPostsForUserSpecialty")]
        public IHttpActionResult GetAllPostsForUserSpecialty()
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.GetById(currentUserId);

            //return Ok();
            return this.GetAllPostsFor(p => p.SpecialtyId == currentUser.SpecialtyId);
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Employer + "," + RoleConstants.Student + "," + RoleConstants.Teacher)]
        [Route("api/Posts/CreateNewSpecialtyPost")]
        public IHttpActionResult CreateNewSpecialtyPost(CreatePostViewModel inputPost)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(GeneralConstants.InvalidRequest);
            }

            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.GetById(currentUserId);

            Specialty specialty = this.Data.Specialties.All().SingleOrDefault(s => s.Name == inputPost.Specialty);

            string roleId = this.Data.Users.All()
                .Select(u => new { Id = u.Id, Role = u.Roles.FirstOrDefault() })
                .FirstOrDefault(u => u.Id == currentUserId).Role.RoleId;
            string roleName = this.Data.Roles.All().First(r => r.Id == roleId).Name;

            int specialtyId;
            if (specialty == null && roleName != RoleConstants.Student)
            {
                throw new ArgumentException("Specialty id not specified.");
            }
            else if(specialty != null)
            {
                specialtyId = specialty.Id;
            }
            else
            {
                specialtyId = (int)currentUser.SpecialtyId;
            }

            Post newPost = new Post()
            {
                Content = inputPost.Content,
                CreatedAt = DateTime.Now,
                SpecialtyId = specialtyId,
                UserFromId = currentUserId
            };

            if(roleName == RoleConstants.Student)
            {
                newPost.GroupId = currentUser.GroupId;
            }

            this.Data.Posts.Add(newPost);
            this.Data.SaveChanges();

            return Ok("New post added.");
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Student)]
        [Route("api/Posts/CreateNewGroupPost")]
        public IHttpActionResult CreateNewGroupPost(CreatePostViewModel inputPost)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(GeneralConstants.InvalidRequest);
            }

            if(inputPost.IsSpecialtyPost == null)
            {
                throw new ArgumentException("Specialty id must be specified.");
            }

            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.GetById(currentUserId);
            
            Post newPost = new Post()
            {
                Content = inputPost.Content,
                CreatedAt = DateTime.Now,
                GroupId = currentUser.GroupId,
                UserFromId = currentUserId
            };

            if((bool)inputPost.IsSpecialtyPost)
            {
                newPost.SpecialtyId = currentUser.SpecialtyId;
            }

            this.Data.Posts.Add(newPost);
            this.Data.SaveChanges();

            return Ok("New post added.");
        }

        private IHttpActionResult GetAllPostsFor(Expression<Func<Post, bool>> whereCondition)
        {
            var result = this.Data.Posts.All()
                .Where(whereCondition)
                .OrderByDescending(p => p.CreatedAt)
                .Select(GetPostsViewModel.FromPost).ToList();

            return Ok(result);
        }
    }
}