namespace SocialT.Web.Controllers
{
    using System.Web.Http;

    using SocialT.Data;
    using TripExchange.Web.Infrastructure;

    public class BaseApiController : ApiController
    {
        //private ApplicationRoleManager _AppRoleManager = null;

        public BaseApiController(ISocialTData data)
        {
            this.Data = data;
        }

        protected ISocialTData Data { get; private set; }

        //protected ApplicationRoleManager AppRoleManager
        //{
        //    get
        //    {
        //        return _AppRoleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
        //    }
        //}
    }
}