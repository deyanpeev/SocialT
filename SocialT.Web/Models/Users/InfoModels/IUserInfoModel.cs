namespace SocialT.Web.Models.Users.InfoModels
{
    public class IUserInfoModel
    {

        public string Id { get; set; }

        public string Email { get; set; }

        
        public bool IsSameUser { get; set; }

        public string RoleName { get; set; }
    }
}