namespace SocialT.Web.Models.Users
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterStudentModel : RegisterBindingModel
    {
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


        [Required]
        public int GroupId { get; set; }
    }
}
