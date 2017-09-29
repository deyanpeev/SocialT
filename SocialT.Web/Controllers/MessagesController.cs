namespace SocialT.Web.Controllers
{
    using Common.Constants;
    using Microsoft.AspNet.Identity;
    using Models.Messages;
    using Services;
    using SocialT.Data;
    using SocialT.Models;
    using SocialT.Web.Controllers;
    using System;
    using System.Linq;
    using System.Web.Http;

    [Authorize]
    [RoutePrefix("api/messages")]
    public class MessagesController : BaseApiController
    {
        public MessagesController()
            : this(new SocialTData())
        {
        }

        public MessagesController(ISocialTData data)
            : base(data)
        {
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.All().FirstOrDefault(x => x.Id == currentUserId);

            var messages = this.Data.Messages.All().Where(m => m.UserToId == currentUserId)
                .OrderByDescending(m => m.CreatedAt).Select(GetMessagesViewModel.FromMessage);

            return Ok(messages);
        }

        [HttpPost]
        [Route("SentMessage")]
        public IHttpActionResult SentMessage(SentMessageViewModel model)
        {
            if(!this.ModelState.IsValid)
            {
                return BadRequest(GeneralConstants.InvalidRequest);
            }

            var currentUserId = User.Identity.GetUserId();
            var currentUser = this.Data.Users.GetById(currentUserId);
            string currentUserName = currentUser.FirstName != null ? currentUser.FirstName + " " + currentUser.LastName
                : currentUser.CompanyName;

            var userTo = this.Data.Users.GetById(model.UserToId);

            string emailContent = "<h2>New message from " + currentUserName
                + " sent.</h2><div>" + model.Content + "<div>";

            GeneralMailMessageService.SendEmail(userTo.Email, model.Subject, emailContent);

            Message newMessage = new Message()
            {
                Subject = model.Subject,
                Content = model.Content,
                UserToId = model.UserToId,
                UserFromId = currentUserId,
                CreatedAt = DateTime.Now
            };

            this.Data.Messages.Add(newMessage);
            this.Data.SaveChanges();

            return Ok("Message successfully sent.");
        }
    }
}