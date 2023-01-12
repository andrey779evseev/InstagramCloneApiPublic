using System.Reflection;
using Application.Behaviors;
using Application.Commands.Auth.ChangePassword;
using Application.Commands.Auth.GoogleLogin;
using Application.Commands.Auth.Login;
using Application.Commands.Auth.Refresh;
using Application.Commands.Auth.Registration;
using Application.Commands.Auth.Revoke;
using Application.Commands.Comments.AddPostComment;
using Application.Commands.Friendships.SearchUsers;
using Application.Commands.Media.SaveImage;
using Application.Commands.Post.CreatePost;
using Application.Commands.User.SetAvatar;
using Application.Commands.User.UpdateUser;
using Application.Queries.Friendships.GetSuggestions;
using Application.Queries.Posts.GetFeed;
using Application.Queries.Posts.GetMiniatures;
using Application.Queries.User.CheckNickname;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddValidators();
        services.AddBehaviors();
        services.AddMediatR(Assembly.GetExecutingAssembly());
        return services;
    }

    private static IServiceCollection AddValidators(
        this IServiceCollection services
    )
    {
        services.AddScoped<IValidator<RegistrationCommand>, RegistrationCommandValidator>();
        services.AddScoped<IValidator<LoginCommand>, LoginCommandValidator>();
        services.AddScoped<IValidator<RefreshTokenCommand>, RefreshTokenCommandValidator>();
        services.AddScoped<IValidator<RevokeTokenCommand>, RevokeTokenCommandValidator>();
        services.AddScoped<IValidator<CheckNicknameQuery>, CheckNicknameQueryValidator>();
        services.AddScoped<IValidator<UpdateUserCommand>, UpdateUserCommandValidator>();
        services.AddScoped<IValidator<ChangePasswordCommand>, ChangePasswordCommandValidator>();
        services.AddScoped<IValidator<GoogleLoginCommand>, GoogleLoginCommandValidator>();
        services.AddScoped<IValidator<GetMiniaturesQuery>, GetMiniaturesQueryValidator>();
        services.AddScoped<IValidator<CreatePostCommand>, CreatePostCommandValidator>();
        services.AddScoped<IValidator<SaveImageCommand>, SaveImageCommandValidator>();
        services.AddScoped<IValidator<AddPostCommentCommand>, AddPostCommentCommandValidator>();
        services.AddScoped<IValidator<SearchUsersCommand>, SearchUsersCommandValidator>();
        services.AddScoped<IValidator<GetFeedQuery>, GetFeedQueryValidator>();
        services.AddScoped<IValidator<GetSuggestionsQuery>, GetSuggestionsQueryValidator>();
        services.AddScoped<IValidator<SetAvatarCommand>, SetAvatarCommandValidator>();
        return services;
    }

    private static IServiceCollection AddBehaviors(
        this IServiceCollection services
    )
    {
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        return services;
    }
}