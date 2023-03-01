using Application.Exceptions;
using Domain.Interfaces.Accessors;
using Domain.Interfaces.Storages;
using Domain.Interfaces.Utils.Logger;
using Domain.Utils.File;
using MediatR;

namespace Application.Commands.Media.SaveImage;

public class SaveImageCommandHandler : IRequestHandler<SaveImageCommand, string>
{
    private readonly IFileUtils _fileUtils;
    private readonly ILogger _logger;
    private readonly ISupabaseStorage _storage;
    private readonly IUserAccessor _userAccessor;

    public SaveImageCommandHandler(
        IUserAccessor userAccessor,
        ISupabaseStorage storage,
        IFileUtils fileUtils,
        ILogger logger
    )
    {
        _userAccessor = userAccessor;
        _storage = storage;
        _fileUtils = fileUtils;
        _logger = logger;
    }

    public async Task<string> Handle(SaveImageCommand command, CancellationToken cancellationToken)
    {
        var user = _userAccessor.Get();
        if (user == null)
            throw new UserDoesNotExistException();

        var allowedExtension = _fileUtils.IsImage(command.File.Name);
        if (!allowedExtension)
            throw new ValidationRequestException("Images with this extension not allowed");

        var url = await _storage.Save(user.Nickname, command.File, command.FileType);

        if (url == null)
            throw new UnexpectedErrorException();

        return url;
    }
}