using Domain.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UserDatabaseConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder
            .HasKey(user => user.Id);
        builder
            .HasIndex(user => user.Nickname)
            .IsUnique();
        builder
            .HasIndex(user => user.Email)
            .IsUnique();
        builder
            .Property(user => user.Id)
            .HasColumnName("id")
            .HasColumnType("uuid");
        builder
            .Property(user => user.Email)
            .HasColumnName("email");
        builder
            .Property(user => user.GoogleId)
            .HasColumnName("google_id");
        builder
            .Property(user => user.Name)
            .HasColumnName("name");
        builder
            .Property(user => user.HashedPassword)
            .HasColumnName("hashed_password");
        builder
            .Property(user => user.Avatar)
            .HasColumnName("avatar");
        builder
            .Property(user => user.Description)
            .HasColumnName("description");
        builder
            .Property(user => user.Gender)
            .HasColumnName("gender");
        builder
            .Property(user => user.Followers)
            .HasColumnName("followers")
            .HasColumnType("jsonb");
        builder
            .Property(user => user.Following)
            .HasColumnName("following")
            .HasColumnType("jsonb");
        builder
            .Property(user => user.Nickname)
            .HasColumnName("nickname");
        builder
            .Property(user => user.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamp");
        builder
            .Property(user => user.UpdatedAt)
            .HasColumnName("updated_at")
            .HasColumnType("timestamp");
    }
}