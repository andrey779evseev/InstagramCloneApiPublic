using Domain.Models.Post;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class PostDatabaseConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("posts");
        builder.HasKey(post => post.Id);
        builder
            .Property(post => post.AuthorId)
            .HasColumnName("author_id")
            .HasColumnType("uuid");
        builder
            .Property(post => post.Description)
            .HasColumnName("description");
        builder
            .Property(post => post.Photo)
            .HasColumnName("photo");
        builder
            .Property(post => post.Id)
            .HasColumnName("id")
            .HasColumnType("uuid");
        builder
            .Property(post => post.PostedAt)
            .HasColumnName("posted_at")
            .HasColumnType("timestamp");
        builder
            .HasOne(post => post.User)
            .WithMany(user => user.Posts)
            .HasForeignKey(t => t.AuthorId);
    }
}