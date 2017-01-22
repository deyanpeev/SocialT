namespace SocialT.Web.Models.Users
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class RegisterTeacherBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required]
        [Display(Name = "First name")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "The first name must be between 2-100")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "The last name must be between 2-100")]
        public string LastName { get; set; }
    }
}