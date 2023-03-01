using Domain.Models.EndpointLog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class EndpointLogDatabaseConfiguration : IEntityTypeConfiguration<EndpointLog>
{
    public void Configure(EntityTypeBuilder<EndpointLog> builder)
    {
        builder.ToTable("endpoints_logs");
        builder.HasKey(log => log.Id);
        builder
            .Property(log => log.Id)
            .HasColumnName("id")
            .HasColumnType("uuid");
        builder
            .Property(log => log.Path)
            .HasColumnName("path");
        builder
            .Property(log => log.Request)
            .HasColumnName("request")
            .HasColumnType("jsonb");
        builder
            .Property(log => log.Response)
            .HasColumnName("response")
            .HasColumnType("jsonb");
        builder
            .Property(log => log.RespondedAt)
            .HasColumnName("responded_at")
            .HasColumnType("timestamp");
        builder
            .Property(log => log.AccessToken)
            .HasColumnName("access_token")
            .HasDefaultValue("Anonymous");
        builder
            .Property(log => log.ErrorDescription)
            .HasColumnName("error_description");
        builder
            .Property(log => log.ExceptionName)
            .HasColumnName("exception_name");
        builder.Ignore(log => log.IsError);
        builder
            .Property(log => log.StatusCode)
            .HasColumnName("status_code");
        builder
            .Property(log => log.UserId)
            .HasColumnName("user_id")
            .HasColumnType("uuid");
        builder
            .Property(log => log.Method)
            .HasColumnName("method");
        builder
            .Property(log => log.ElapsedTime)
            .HasColumnName("elapsed_time");
        builder
            .Property(log => log.HandlerName)
            .HasColumnName("handler_name");
        builder
            .HasOne(post => post.User)
            .WithMany(user => user.EndpointLogs)
            .HasForeignKey(t => t.UserId);
    }
}