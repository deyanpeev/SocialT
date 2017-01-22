namespace SocialT.Web.Models.Posts
{
    using System.ComponentModel.DataAnnotations;

    public class CreatePostViewModel
    {
        [Required]
        [StringLength(300, MinimumLength = 5)]
        public string Content { get; set; }

        public string Specialty { get; set; }

        public bool? IsSpecialtyPost { get; set; }
    }
}