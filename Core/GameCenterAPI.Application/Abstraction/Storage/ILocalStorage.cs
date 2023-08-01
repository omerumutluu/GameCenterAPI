using Microsoft.AspNetCore.Http;

namespace GameCenterAPI.Application.Abstraction.Storage
{
    public interface ILocalStorage
    {
        string UploadFile(string path, IFormFile file);
        List<(string path, string fileName)> UploadFile(string path, IFormFileCollection file);
        string RenameFile(string path, string fileName, bool isFirst = true);
    }
}
