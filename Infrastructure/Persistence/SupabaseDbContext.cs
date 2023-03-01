using System.Reflection;
using Domain.Models.Comment;
using Domain.Models.EndpointLog;
using Domain.Models.Like;
using Domain.Models.Post;
using Domain.Models.RefreshToken;
using Domain.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence;

public class SupabaseDbContext : DbContext
{
    //dotnet ef migrations add v1 --project Infrastructure --output-dir Persistence/Migrations
    //dotnet ef database update --project Infrastructure
    public SupabaseDbContext() : base()
    {
    }

    public SupabaseDbContext(DbContextOptions<SupabaseDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<Like> Likes { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<EndpointLog> EndpointLogs { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddJsonFile(
                $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
                optional: true)
            // .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "../Api/appsettings.Development.json"))
            // .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "../Api/appsettings.DevelopmentLocal.json"))
            .Build();
        optionsBuilder.UseNpgsql(configuration.GetConnectionString("SupabaseCredentials"));
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}