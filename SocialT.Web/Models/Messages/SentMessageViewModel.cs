using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialT.Web.Models.Messages
{
    public class SentMessageViewModel
    {
        [Required]
        public string UserToId { get; set; }

        [Required]
        [StringLength(300, MinimumLength = 5)]
        public string Content { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Subject { get; set; }
    }
}