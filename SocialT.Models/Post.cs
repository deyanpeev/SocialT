namespace SocialT.Models
{
    using System;

    public class Post
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string UserFromId { get; set; }

        public virtual ApplicationUser UserFrom { get; set; }

        public int? SpecialtyId { get; set; }

        public Specialty Specialty { get; set; }

        public int? GroupId { get; set; }

        public virtual Group Group { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
