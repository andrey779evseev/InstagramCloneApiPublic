using Domain.Interfaces.Accessors;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Storages;
using Domain.Interfaces.Utils.GoogleTokenValidator;
using Domain.Interfaces.Utils.Logger;
using Domain.Interfaces.Utils.PasswordHasher;
using Domain.Interfaces.Utils.Tokens;
using Domain.Settings.Storages;
using Domain.Settings.Utils.PasswordHasher;
using Domain.Utils.File;
using Infrastructure.Accessors;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Infrastructure.Storages;
using Infrastructure.Utils.File;
using Infrastructure.Utils.GoogleTokenValidator;
using Infrastructure.Utils.Logger;
using Infrastructure.Utils.PasswordHasher;
using Infrastructure.Utils.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Supabase;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddUtils();
        services.AddRepositories();
        services.AddDb(configuration);
        services.AddIOptions(configuration);
        services.AddStorages(configuration);

        return services;
    }

    private static IServiceCollection AddUtils(
        this IServiceCollection services
    )
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ITokenGenerator, TokenGenerator>();
        services.AddScoped<IAccessTokenService, AccessTokenService>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();
        services.AddScoped<IAuthenticateService, AuthenticateService>();
        services.AddScoped<IUserAccessor, UserAccessor>();
        services.AddScoped<IGoogleTokenValidator, GoogleTokenValidator>();
        services.AddScoped<IGoogleTokenValidator, GoogleTokenValidator>();
        services.AddScoped<IFileUtils, FileUtils>();
        services.AddScoped<ILogger, Logger>();
        return services;
    }

    private static IServiceCollection AddStorages(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var settings = new SupabaseSettings();
        configuration.Bind(nameof(SupabaseSettings), settings);
        services.AddSingleton(settings);
        
        services.AddSingleton<ISupabaseStorage, SupabaseStorage>();

        return services;
    }

    private static IServiceCollection AddRepositories(
        this IServiceCollection services
    )
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<ILikeRepository, LikeRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<IEndpointLogRepository, EndpointLogRepository>();
        return services;
    }

    private static IServiceCollection AddDb(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<SupabaseDbContext>(opt
            => opt.UseNpgsql(configuration.GetConnectionString("SupabaseCredentials")));
        return services;
    }

    private static IServiceCollection AddIOptions(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.Configure<HashingSettings>(configuration.GetSection(nameof(HashingSettings)));
        return services;
    }
}