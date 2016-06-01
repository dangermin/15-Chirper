using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace Chirper.API.Models
{
    public class Chirp
    {
        // Primary Key
        public int ChirpId { get; set; }
        public string UserId { get; set; }

       // Fields relevant to the Chirp
       public string ChirpText { get; set; }
       public DateTime DateCreated { get; set; }
       // public string Comment { get; set; } 
       public int LikeCount { get; set; } 

       // Relationship fields
       public virtual ChirperUser User { get; set; }
       public virtual ICollection<Comment> Comments { get; set; } 
    }
}