using System;
using Chirper.API.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Chirper.API.Infrastructure
{
    public class ChirperDataContext : IdentityDbContext<ChirperUser>
    {
        public ChirperDataContext() : base("Chirper")
        {

        }

        public IDbSet<Chirp> Chirps { get; set; }
        public IDbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chirp>()
                // Creating 1-to-many relationships between Chirp and Comments
                .HasMany(chirp => chirp.Comments)
                .WithRequired(comment => comment.Chirp)
                .HasForeignKey(comment => comment.ChirpId);

            // Create 1-* relationship between User and Chirp
            modelBuilder.Entity<ChirperUser>()
                .HasMany(u => u.Post)
                .WithRequired(c => c.User)
                .HasForeignKey(c => c.UserId);

            // Create 1-* relationship between User and Comment
            modelBuilder.Entity<ChirperUser>()
                .HasMany(u => u.Comments)
                .WithRequired(c => c.User)
                .HasForeignKey(c => c.UserId)
                .WillCascadeOnDelete(false);

            // Be sure to 
            base.OnModelCreating(modelBuilder);
        }
    }
}