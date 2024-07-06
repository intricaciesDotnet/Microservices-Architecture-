using Microsoft.EntityFrameworkCore;

namespace WebApplication.User.API.ApplicationDb;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Application.Shared.Entities.User> Users { get; set; }
}
