using Domain.Models.Comment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class CommentDatabaseConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("comments");
        builder.HasKey(comment => comment.Id);
        builder
            .Property(comment => comment.Id)
            .HasColumnName("id")
            .HasColumnType("uuid");
        builder
            .Property(comment => comment.PostId)
            .HasColumnName("post_id")
            .HasColumnType("uuid");
        builder
            .Property(comment => comment.UserId)
            .HasColumnName("user_id")
            .HasColumnType("uuid");
        builder
            .Property(comment => comment.CommentedAt)
            .HasColumnName("commented_at")
            .HasColumnType("timestamp");
        builder
            .Property(comment => comment.Text)
            .HasColumnName("text");
        builder
            .HasOne(post => post.User)
            .WithMany(user => user.Comments)
            .HasForeignKey(t => t.UserId);
        builder
            .HasOne(post => post.Post)
            .WithMany(user => user.Comments)
            .HasForeignKey(t => t.PostId);
    }
}