using Freepository.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Freepository.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Resource> Resources { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ResourceTag> ResourceTags { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configurar clave compuesta para ResourceTag
            modelBuilder.Entity<ResourceTag>()
                .HasKey(rt => new { rt.ResourceId, rt.TagId });

            // Configurar relaciones para ResourceTag
            modelBuilder.Entity<ResourceTag>()
                .HasOne(rt => rt.Resource)
                .WithMany(r => r.ResourceTags)
                .HasForeignKey(rt => rt.ResourceId);

            modelBuilder.Entity<ResourceTag>()
                .HasOne(rt => rt.Tag)
                .WithMany(t => t.ResourceTags)
                .HasForeignKey(rt => rt.TagId);
            // Configurar la relación entre Resource y User
            // modelBuilder.Entity<Resource>()
            //     .HasOne(r => r.User)
            //     .WithMany(u => u.Resources)
            //     .HasForeignKey(r => r.UserId);

            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}