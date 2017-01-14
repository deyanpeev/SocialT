namespace SocialT.Models
{
    using System;

    public class Message
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string UserFromId { get; set; }

        public virtual ApplicationUser UserFrom { get; set; }

        public string SpecialtyName { get; set; }

        public string UserToId { get; set; }

        public virtual ApplicationUser UserTo { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
