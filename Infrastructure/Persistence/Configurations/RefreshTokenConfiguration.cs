using Domain.Models.RefreshToken;
using Domain.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("refresh_tokens");
        builder.HasKey(token => token.Token);
        builder
            .Property(token => token.Token)
            .HasColumnName("token");
        builder
            .Property(token => token.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("timestamp");
        builder
            .Property(token => token.ExpiresAt)
            .HasColumnName("expires_at")
            .HasColumnType("timestamp");
        builder
            .Property(token => token.UserId)
            .HasColumnName("user_id");
        builder
            .Property(token => token.Revoked)
            .HasColumnName("revoked");
        builder
            .HasOne(token => token.User)
            .WithMany(user => user.RefreshTokens)
            .HasForeignKey(t => t.UserId);
    }
}