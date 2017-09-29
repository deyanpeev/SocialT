namespace SocialT.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Http;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.Owin.Security.OAuth;

    using SocialT.Data;
    using SocialT.Models;
    using SocialT.Web.Models.Users;
    using SocialT.Web.Providers;
    using SocialT.Web.Results;
    using System.Web.Security;
    using SocialT.Common.Constants;
    using App_Start;
    using Microsoft.Owin.Security.DataProtection;
    using SocialT.Web.Services;
    using Models.Users.InfoModels;

    [Authorize]
    [RoutePrefix("api/users")]
    public class UsersController : BaseApiController
    {
        private const string LocalLoginProvider = "Local";

        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;

        public UsersController()
            : base(new SocialTData())
        {
            this.userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(new ApplicationDbContext()));

            this.roleManager = new ApplicationRoleManager(new RoleStore<IdentityRole>(new ApplicationDbContext()));
        }

        public UsersController(
            ApplicationUserManager userManager,
            ISecureDataFormat<AuthenticationTicket> accessTokenFormat,
            ISocialTData data)
            : base(data)
        {
            this.UserManager = userManager;
            this.AccessTokenFormat = accessTokenFormat;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return this.userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }

            private set
            {
                this.userManager = value;
            }
        }

        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }

        private IAuthenticationManager Authentication
        {
            get
            {
                return Request.GetOwinContext().Authentication;
            }
        }

        [HttpGet]
        [Route("GetUserById")]
        public IHttpActionResult GetUserById()
        {
            return this.GetUserById(User.Identity.GetUserId());
        }

        [HttpGet]
        [Route("GetUserById")]
        public IHttpActionResult GetUserById(string id)
        {
            string currenUserId = User.Identity.GetUserId();

            string roleId = this.Data.Users.All()
                .Select(u => new { Id = u.Id, Role = u.Roles.FirstOrDefault() })
                .FirstOrDefault(u => u.Id == id).Role.RoleId;
            string roleName = this.Data.Roles.All().First(r => r.Id == roleId).Name;
            //string userRole = this.Data.Roles.All().FirstOrDefault(r => r.Id == roleId).Name;
            var user = this.Data.Users.GetById(id);

            IUserInfoModel result = null;

            switch (roleName)
            {
                case RoleConstants.Student:
                    result = this.Data.Users.All().Select(StudentInfoModel.FromAppUser).FirstOrDefault(u => u.Id == id);
                    break;
                case RoleConstants.Employer:
                    result = this.Data.Users.All().Select(EmployerInfoModel.FromAppUser).FirstOrDefault(u => u.Id == id);
                    break;
                default:
                    result = this.Data.Users.All().Select(IUserInfoModel.FromAppUser).FirstOrDefault(u => u.Id == id);
                    break;
            }

            result.IsSameUser = currenUserId == id;
            result.RoleName = roleName;
            return Ok(result);
        }

        //TODO Depricate
        // POST api/Users/Register
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            //if (!model.IsDriver)
            //{
            //    model.Car = null;
            //}

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                //IsDriver = model.IsDriver,
                //Car = model.Car
            };

            IdentityResult result = await this.UserManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return this.GetErrorResult(result);
            }

            Uri locationHeader = this.SendActivationMail(user);

            return Created(locationHeader, user);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("RegisterStudent")]
        public async Task<IHttpActionResult> RegisterStudent(RegisterStudentModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            int specialtyId = this.Data.Specialties.All().Single(s => s.Name == model.Specialty).Id;

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                FacultyNumber = model.FacultyNumber,
                Course = model.Course,
                GroupId = model.GroupId,
                SpecialtyId = specialtyId,
                IsActive = false
            };

            IdentityResult result = await this.UserManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return this.GetErrorResult(result);
            }

            userManager.AddToRole(user.Id, RoleConstants.Student);

            Uri locationHeader = this.SendActivationMail(user);

            this.Data.SaveChanges();

            return Created(locationHeader, user);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("RegisterEmployer")]
        public async Task<IHttpActionResult> RegisterEmployer(RegisterEmployerModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                Description = model.Description,
                CompanyName = model.CompanyName,
                CompanyMoto = model.CompanyMoto,
                IsActive = false
            };

            IdentityResult result = await this.UserManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return this.GetErrorResult(result);
            }

            userManager.AddToRole(user.Id, RoleConstants.Employer);

            Uri locationHeader = this.SendActivationMail(user);

            this.Data.SaveChanges();

            return Created(locationHeader, user);
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Admin)]
        [Route("RegisterTeacher")]
        public async Task<IHttpActionResult> RegisterTeacher(RegisterTeacherBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                IsActive = true,
                EmailConfirmed = true
            };

            IdentityResult result = await this.UserManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                return this.GetErrorResult(result);
            }

            userManager.AddToRole(user.Id, RoleConstants.Teacher);

            this.Data.SaveChanges();

            return Ok("Teacher successfully registered");
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("ConfirmEmail", Name = "ConfirmEmailRoute")]
        public async Task<IHttpActionResult> ConfirmEmail(string userId = "", string code = "")
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
            {
                return BadRequest("User Id and Code are required");
            }

            if (this.UserManager.UserTokenProvider == null)
            {
                var provider = new DpapiDataProtectionProvider("SocialT");
                this.UserManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(provider.Create("EmailConfirmation"));
            }
            this.Data.Users.GetById(userId).EmailConfirmed = true;
            this.Data.SaveChanges();

            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = RoleConstants.Admin + "," + RoleConstants.Teacher)]
        [Route("ChangeUserActiveState")]
        public IHttpActionResult ChangeUserActiveState(string userId, bool active)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return BadRequest("The user ID cannot be null or empty.");
            }

            var user = this.Data.Users.All().FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return BadRequest("User with that ID does not exist.");
            }

            user.IsActive = active;
            this.Data.SaveChanges();

            GeneralMailMessageService.SendEmail(user.Email, "SocialT User was activated", "Your account was " + (active ? "activated" : "deactivated"));

            return Ok("Successfully changed user acive status.");
        }

        [HttpGet]
        //[Authorize(Roles = RoleConstants.Admin)]
        [Route("GetUserByActivation")]
        public IHttpActionResult GetUserByActivation()
        {
            string adminRoleId = this.Data.Roles.All().First(r => r.Name == RoleConstants.Admin).Id;
            string teacherRoleId = this.Data.Roles.All().First(r => r.Name == RoleConstants.Teacher).Id;
            var allUsers = this.Data.Users.All().Where(u => u.Roles.Any(r => r.RoleId != adminRoleId && r.RoleId != teacherRoleId))
                .OrderBy(u => !u.IsActive).ThenBy(u => u.Email)
                .Select(ActivationUserViewModel.FromApplicationUser).ToList();
            foreach (var user in allUsers)
            {
                string roleName = this.Data.Roles.All().First(r => r.Id == user.RoleId).Name;
                user.Role = roleName;
            }
            return Ok(allUsers);
        }

        [Route("user/{id:guid}", Name = "GetUserById")]
        public async Task<IHttpActionResult> GetUser(string Id)
        {
            ApplicationUser user = await this.UserManager.FindByIdAsync(Id);

            if (user != null)
            {
                return Ok(user);
            }

            return NotFound();

        }

        [Route("user/{username}")]
        public async Task<IHttpActionResult> GetUserByName(string username)
        {
            var user = await this.UserManager.FindByNameAsync(username);

            if (user != null)
            {
                return Ok(user);
            }

            return NotFound();

        }

        // POST api/Users/Logout
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            this.Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return this.Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("SetTeacherPassword")]
        public async Task<IHttpActionResult> SetTeacherPassword([FromUri]string userId, SetPasswordBindingModel passwordModel)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            if (!this.Data.Users.All().Any(u => u.Id == userId))
            {
                return BadRequest("No such user found");
            }

            IdentityResult result = await this.UserManager.AddPasswordAsync(userId, passwordModel.NewPassword);

            if (!result.Succeeded)
            {
                return this.GetErrorResult(result);
            }

            return this.Ok("Teacher password successfully set.");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.UserManager.Dispose();
            }

            base.Dispose(disposing);
        }

        // GET api/Users/ManageInfo?returnUrl=%2F&generateState=true
        [Route("ManageInfo")]
        private async Task<ManageInfoViewModel> GetManageInfo(string returnUrl, bool generateState = false)
        {
            IdentityUser user = await this.UserManager.FindByIdAsync(User.Identity.GetUserId());

            if (user == null)
            {
                return null;
            }

            var logins = new List<UserLoginInfoViewModel>();

            foreach (IdentityUserLogin linkedAccount in user.Logins)
            {
                logins.Add(
                    new UserLoginInfoViewModel
                    {
                        LoginProvider = linkedAccount.LoginProvider,
                        ProviderKey = linkedAccount.ProviderKey
                    });
            }

            if (user.PasswordHash != null)
            {
                logins.Add(
                    new UserLoginInfoViewModel { LoginProvider = LocalLoginProvider, ProviderKey = user.UserName, });
            }

            return new ManageInfoViewModel
            {
                LocalLoginProvider = LocalLoginProvider,
                Email = user.UserName,
                Logins = logins,
                ExternalLoginProviders = this.GetExternalLogins(returnUrl, generateState)
            };
        }

        // POST api/Users/ChangePassword
        [Route("ChangePassword")]
        private async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            IdentityResult result =
                await this.UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                return this.GetErrorResult(result);
            }

            return this.Ok();
        }

        // POST api/Users/SetPassword
        [Route("SetPassword")]
        private async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var result = await this.UserManager.AddPasswordAsync(User.Identity.GetUserId(), model.NewPassword);

            if (!result.Succeeded)
            {
                return this.GetErrorResult(result);
            }

            return this.Ok();
        }

        // POST api/Users/AddExternalLogin
        [Route("AddExternalLogin")]
        private async Task<IHttpActionResult> AddExternalLogin(AddExternalLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            this.Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

            AuthenticationTicket ticket = this.AccessTokenFormat.Unprotect(model.ExternalAccessToken);

            if (ticket == null || ticket.Identity == null
                || (ticket.Properties != null && ticket.Properties.ExpiresUtc.HasValue
                    && ticket.Properties.ExpiresUtc.Value < DateTimeOffset.UtcNow))
            {
                return this.BadRequest("External login failure.");
            }

            var externalData = ExternalLoginData.FromIdentity(ticket.Identity);

            if (externalData == null)
            {
                return this.BadRequest("The external login is already associated with an account.");
            }

            var result =
                await
                this.UserManager.AddLoginAsync(
                    User.Identity.GetUserId(),
                    new UserLoginInfo(externalData.LoginProvider, externalData.ProviderKey));

            if (!result.Succeeded)
            {
                return this.GetErrorResult(result);
            }

            return this.Ok();
        }

        // POST api/Users/RemoveLogin
        [Route("RemoveLogin")]
        private async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            IdentityResult result;

            if (model.LoginProvider == LocalLoginProvider)
            {
                result = await this.UserManager.RemovePasswordAsync(User.Identity.GetUserId());
            }
            else
            {
                result =
                    await
                    this.UserManager.RemoveLoginAsync(
                        User.Identity.GetUserId(),
                        new UserLoginInfo(model.LoginProvider, model.ProviderKey));
            }

            if (!result.Succeeded)
            {
                return this.GetErrorResult(result);
            }

            return this.Ok();
        }

        // GET api/Users/ExternalLogin
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        private async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        {
            if (error != null)
            {
                return this.Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
            }

            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this);
            }

            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return this.InternalServerError();
            }

            if (externalLogin.LoginProvider != provider)
            {
                this.Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new ChallengeResult(provider, this);
            }

            ApplicationUser user =
                await this.UserManager.FindAsync(new UserLoginInfo(externalLogin.LoginProvider, externalLogin.ProviderKey));

            bool hasRegistered = user != null;

            if (hasRegistered)
            {
                this.Authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);

                var oauthIdentity = await user.GenerateUserIdentityAsync(this.UserManager, OAuthDefaults.AuthenticationType);
                var cookieIdentity =
                    await user.GenerateUserIdentityAsync(this.UserManager, CookieAuthenticationDefaults.AuthenticationType);

                AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName,
                    roleManager.FindByIdAsync(user.Roles.FirstOrDefault().RoleId).Result.Name);
                this.Authentication.SignIn(properties, oauthIdentity, cookieIdentity);
            }
            else
            {
                IEnumerable<Claim> claims = externalLogin.GetClaims();
                var identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
                this.Authentication.SignIn(identity);
            }

            return this.Ok();
        }

        // GET api/Users/ExternalLogins?returnUrl=%2F&generateState=true
        [AllowAnonymous]
        [Route("ExternalLogins")]
        private IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        {
            IEnumerable<AuthenticationDescription> descriptions = this.Authentication.GetExternalAuthenticationTypes();
            var logins = new List<ExternalLoginViewModel>();

            string state;

            if (generateState)
            {
                const int StrengthInBits = 256;
                state = RandomOAuthStateGenerator.Generate(StrengthInBits);
            }
            else
            {
                state = null;
            }

            foreach (AuthenticationDescription description in descriptions)
            {
                var login = new ExternalLoginViewModel
                {
                    Name = description.Caption,
                    Url =
                                        Url.Route(
                                            "ExternalLogin",
                                            new
                                            {
                                                provider = description.AuthenticationType,
                                                response_type = "token",
                                                client_id = Startup.PublicClientId,
                                                redirect_uri =
                                        new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
                                                state = state
                                            }),
                    State = state
                };
                logins.Add(login);
            }

            return logins;
        }

        // POST api/Users/RegisterExternal
        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("RegisterExternal")]
        private async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var info = await this.Authentication.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return this.InternalServerError();
            }

            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };

            IdentityResult result = await this.UserManager.CreateAsync(user);
            if (!result.Succeeded)
            {
                return this.GetErrorResult(result);
            }

            result = await this.UserManager.AddLoginAsync(user.Id, info.Login);
            if (!result.Succeeded)
            {
                return this.GetErrorResult(result);
            }

            return this.Ok();
        }

        #region Helpers

        private Uri SendActivationMail(ApplicationUser user)
        {
            if (this.UserManager.UserTokenProvider == null)
            {
                var provider = new DpapiDataProtectionProvider("SocialT");
                this.UserManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(provider.Create("EmailConfirmation"))
                {
                    TokenLifespan = TimeSpan.FromHours(3)
                };
            }
            this.UserManager.EmailService = new IdentityMessageService();
            var code = UserManager.GenerateEmailConfirmationToken(user.Id);
            code = System.Web.HttpUtility.UrlEncode(code);
            var callbackUrl = new Uri(Url.Link("ConfirmEmailRoute", new { userId = user.Id, code = code }));
            //TODO uncomment
            this.UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>").Wait();
            Uri locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));

            return locationHeader;
            //return null;
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return this.InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        this.ModelState.AddModelError(string.Empty, error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return this.BadRequest();
                }

                return this.BadRequest(this.ModelState);
            }

            return null;
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int BitsPerByte = 8;

                if (strengthInBits % BitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / BitsPerByte;

                var data = new byte[strengthInBytes];
                random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }

            public string ProviderKey { get; set; }

            public string UserName { get; set; }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || string.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || string.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, this.ProviderKey, null, this.LoginProvider));

                if (this.UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, this.UserName, null, this.LoginProvider));
                }

                return claims;
            }
        }

        #endregion
    }
}