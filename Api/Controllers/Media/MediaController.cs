using Application.Commands.Media.SaveImage;
using Domain.Enums.Storages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Media;

[Authorize]
[Route("api/media")]
public class MediaController : BaseController
{
    /// <summary>
    /// Save provided file image to storage
    /// </summary>
    [HttpPost("save-image")]
    public async Task<IActionResult> SaveImage(IFormFile file, [FromQuery] SupabaseStorageFileTypeEnum fileType,
        CancellationToken cancellationToken)
    {
        var command = new SaveImageCommand(file, fileType);
        var url = await Mediator.Send(command, cancellationToken);
        return Ok(url);
    }
}