using Domain.Models.Like;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class LikeDatabaseConfiguration : IEntityTypeConfiguration<Like>
{
    public void Configure(EntityTypeBuilder<Like> builder)
    {
        builder.ToTable("likes");
        builder.HasKey(like => like.Id);
        builder
            .Property(like => like.Id)
            .HasColumnName("id")
            .HasColumnType("uuid");
        builder
            .Property(like => like.PostId)
            .HasColumnName("post_id")
            .HasColumnType("uuid");
        builder
            .Property(like => like.UserId)
            .HasColumnName("user_id")
            .HasColumnType("uuid");
        builder
            .Property(like => like.LikedAt)
            .HasColumnName("liked_at")
            .HasColumnType("timestamp");
        builder
            .HasOne(post => post.User)
            .WithMany(user => user.Likes)
            .HasForeignKey(t => t.UserId);
        builder
            .HasOne(post => post.Post)
            .WithMany(user => user.Likes)
            .HasForeignKey(t => t.PostId);
    }
}