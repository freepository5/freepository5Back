using Freepository.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Freepository.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { 
       
        }
        
                        public DbSet<Resource> Resources { get; set; }
                        public DbSet<Tag> Tags { get; set; }
        //                 public DbSet<ResourceTag> ResourceTags { get; set; }
        //
        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     base.OnModelCreating(modelBuilder);
        //     modelBuilder.Entity<ResourceTag>()
        //         .HasKey(rt => new { rt.ResourceId, rt.TagId });
        //
        //     modelBuilder.Entity<ResourceTag>()
        //         .HasOne(rt => rt.Resource)
        //         .WithMany(r => r.ResourceTags)
        //         .HasForeignKey(rt => rt.ResourceId);
        //
        //     modelBuilder.Entity<ResourceTag>()
        //         .HasOne(rt => rt.Tag)
        //         .WithMany(t => t.ResourceTags)
        //         .HasForeignKey(rt => rt.TagId);
        //
        //     modelBuilder.Entity<Resource>()
        //         .HasOne(r => r.User)
        //         .WithMany(u => u.Resources)
        //         .HasForeignKey(r => r.UserId);
        //
        // }
    }
}