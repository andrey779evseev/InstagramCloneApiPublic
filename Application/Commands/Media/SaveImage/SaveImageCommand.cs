using Domain.Enums.Storages;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Commands.Media.SaveImage;

public record SaveImageCommand(IFormFile File, SupabaseStorageFileTypeEnum FileType) : IRequest<string>;