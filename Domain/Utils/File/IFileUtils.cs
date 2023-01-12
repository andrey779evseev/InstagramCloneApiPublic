namespace Domain.Utils.File;

public interface IFileUtils
{
    public string GetExtension(string filename);
    public bool IsImage(string filename);
}