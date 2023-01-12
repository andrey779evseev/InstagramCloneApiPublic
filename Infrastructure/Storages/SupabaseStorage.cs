using Domain.Enums.Storages;
using Domain.Interfaces.Storages;
using Domain.Settings.Storages;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Supabase;

namespace Infrastructure.Storages;

public class SupabaseStorage : ISupabaseStorage
{
    private readonly SupabaseSettings _settings;

    public SupabaseStorage(SupabaseSettings settings)
    {
        _settings = settings;
    }

    public async Task<string?> Save(string nickname, IFormFile file, SupabaseStorageFileTypeEnum fileType)
    {
        var supabase = new Client(_settings.Url, _settings.AnonKey);
        await supabase.InitializeAsync();
        var bucket = supabase.Storage.From("assets");
        var bytes = await file.GetBytes();
        var fileName = file.FileName.ConvertToLatin();
        var guid = Guid.NewGuid();
        var url = await bucket.Upload(bytes, $"{nickname}/{GetBasePath(fileType)}/{guid}{fileName}");
        return $"{_settings.Url}/storage/v1/object/public/{url}";
    }

    private string GetBasePath(SupabaseStorageFileTypeEnum fileType)
    {
        return fileType switch
        {
            SupabaseStorageFileTypeEnum.Avatar => "avatars",
            SupabaseStorageFileTypeEnum.PostPicture => "post-pictures",
            _ => "default-folder"
        };
    }
}