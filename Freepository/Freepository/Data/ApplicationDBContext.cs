using Microsoft.EntityFrameworkCore;

namespace DefaultNamespace;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Resource> Resources { get; set; }
}