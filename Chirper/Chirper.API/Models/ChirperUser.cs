using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Chirper.API.Models
{
    public class ChirperUser : IdentityUser
    {
        public virtual string ChirpName { get; set; }

        // Stup our relationships
        public virtual ICollection<Chirp> Post { get; set; } 
        public virtual ICollection<Comment> Comments { get; set; } 
    }
}