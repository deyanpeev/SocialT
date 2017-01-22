namespace SocialT.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Post
    {
        public Post()
        {
            this.Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }

        public string Content { get; set; }

        public string UserFromId { get; set; }

        public virtual ApplicationUser UserFrom { get; set; }

        public int? SpecialtyId { get; set; }

        public virtual Specialty Specialty { get; set; }

        public int? GroupId { get; set; }

        public virtual Group Group { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
