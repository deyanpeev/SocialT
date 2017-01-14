namespace SocialT.Models
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() { }
        public ApplicationRole(string name) : base(name) { }
    }
}
