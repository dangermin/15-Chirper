using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chirper.API.Models
{
    public class Comment
    {
        // Primary Key
        public int CommentId { get; set; }
        // Foriegn Key
        public int ChirpId { get; set; }
        public string UserId { get; set; }
        
        public DateTime DateCreated { get; set; }
        public string Text { get; set; } 

        // Relationship Key
        public virtual Chirp Chirp { get; set; }
        public virtual ChirperUser User { get; set; }
    }
}