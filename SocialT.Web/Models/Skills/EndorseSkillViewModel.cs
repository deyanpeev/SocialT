namespace SocialT.Web.Models.Skills
{
    using System.ComponentModel.DataAnnotations;


    public class EndorseSkillViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public int SkillId { get; set; }
    }
}