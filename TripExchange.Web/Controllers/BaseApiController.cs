namespace SocialUnion.Web.Controllers
{
    using System.Web.Http;

    using SocialUnion.Data;
    using TripExchange.Web.Infrastructure;

    public class BaseApiController : ApiController
    {
        //private ApplicationRoleManager _AppRoleManager = null;

        public BaseApiController(ISocialUnionData data)
        {
            this.Data = data;
        }

        protected ISocialUnionData Data { get; private set; }

        //protected ApplicationRoleManager AppRoleManager
        //{
        //    get
        //    {
        //        return _AppRoleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
        //    }
        //}
    }
}