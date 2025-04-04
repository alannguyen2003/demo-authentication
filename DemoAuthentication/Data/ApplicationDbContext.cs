using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DemoAuthentication.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    private string GetConnectionString()
    {
        IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
        return configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
    }   
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(GetConnectionString());
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<IdentityRole>(entity =>
        {
            entity.Property(r => r.Id).HasMaxLength(450);
        });
        builder.Entity<IdentityUserLogin<string>>(entity =>
        {
            entity.HasKey(x => new { x.LoginProvider, x.ProviderKey });
        });

        builder.Entity<IdentityUserRole<string>>(entity =>
        {
            entity.HasKey(x => new { x.UserId, x.RoleId });
        });

        builder.Entity<IdentityUserToken<string>>(entity =>
        {
            entity.HasKey(x => new { x.UserId, x.LoginProvider, x.Name });
        });
    }
}