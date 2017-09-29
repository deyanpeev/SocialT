using System;

namespace SocialT.Models
{
    public class News
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
