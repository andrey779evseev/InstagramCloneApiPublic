using MediatR;

namespace Application.Commands.User.SetAvatar;

public record SetAvatarCommand(string Url) : IRequest<string>;