namespace SocialT.Web.Controllers
{
    using SocialT.Data;
    using System.Web.Http;
    using System.Linq;
    using Models.News;
    using Common.Constants;
    using SocialT.Models;
    using System;
    
    [RoutePrefix("api/news")]
    public class NewsController : BaseApiController
    {
        public NewsController()
            : this(new SocialTData())
        {
        }

        public NewsController(ISocialTData data)
            : base(data)
        {
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var news = this.Data.News.All().OrderByDescending(n => n.CreatedAt).Select(GetNewsViewModel.FromNews).Take(5);

            return Ok(news);
        }

        [HttpPost]
        [Route("CreateNews")]
        [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.Teacher)]
        public IHttpActionResult CreateNews(CreateNewsViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(GeneralConstants.InvalidRequest);
            }

            this.Data.News.Add(new News()
            {
                Content = model.Content,
                Subject = model.Subject,
                CreatedAt = DateTime.Now
            });
            this.Data.SaveChanges();

            return Ok("Successfully inserted");
        }
    }
}