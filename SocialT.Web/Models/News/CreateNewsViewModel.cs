using System.ComponentModel.DataAnnotations;

namespace SocialT.Web.Models.News
{
    public class CreateNewsViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string Subject { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 5)]
        public string Content { get; set; }
    }
}