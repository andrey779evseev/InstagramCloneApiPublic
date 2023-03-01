using Application.Exceptions;
using Domain.Interfaces.Accessors;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Storages;
using Domain.Interfaces.Utils.Logger;
using Domain.Utils.File;
using MediatR;

namespace Application.Commands.User.SetAvatar;

public class SetAvatarCommandHandler : IRequestHandler<SetAvatarCommand, string>
{
    private readonly IFileUtils _fileUtils;
    private readonly ILogger _logger;
    private readonly ISupabaseStorage _storage;
    private readonly IUserAccessor _userAccessor;
    private readonly IUserRepository _userRepository;

    public SetAvatarCommandHandler(
        IUserRepository userRepository,
        IUserAccessor userAccessor,
        ISupabaseStorage storage,
        IFileUtils fileUtils,
        ILogger logger
    )
    {
        _userRepository = userRepository;
        _userAccessor = userAccessor;
        _storage = storage;
        _fileUtils = fileUtils;
        _logger = logger;
    }

    public async Task<string> Handle(SetAvatarCommand command, CancellationToken cancellationToken)
    {
        var user = _userAccessor.Get();
        if (user == null)
            throw new UserDoesNotExistException();

        var allowedExtension = _fileUtils.IsImage(command.Url);
        if (!allowedExtension)
            throw new ValidationRequestException("Images with this extension not allowed");

        user.SetAvatar(command.Url);
        await _userRepository.Save(user, cancellationToken);

        return user.Avatar!;
    }
}