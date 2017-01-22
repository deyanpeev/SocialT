namespace SocialT.Web.Models.Users
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public class RegisterEmployerModel : RegisterBindingModel
    {
        [StringLength(600)]
        public string Description { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string CompanyName { get; set; }

        [StringLength(100, MinimumLength = 3)]
        public string CompanyMoto { get; set; }

        public List<string> Positions { get; set; }
    }
}
