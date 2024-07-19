using Freepository.Models;
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
                        
                        
 
    }
    
}