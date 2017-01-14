namespace SocialT.Web.Models.Users
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterStudentModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "First name")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "The first name must be between 2-100")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "The last name must be between 2-100")]
        public string LastName { get; set; }

        [Required]
        [Range(1, 4)]
        public int Course { get; set; }

        [Required]
        [StringLength(9)]
        public string FacultyNumber { get; set; }

        [Required]
        public string Specialty { get; set; }
    }
}
