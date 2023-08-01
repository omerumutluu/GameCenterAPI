using Microsoft.AspNetCore.Http;

namespace GameCenterAPI.Application.Abstraction.Storage
{
    public interface IAzureStorage
    {
        Task<List<(string fileName, string containerName)>> UploadAsync(string containerName, IFormFileCollection files);
        List<string> GetFiles(string containerName);
        bool HasFile(string containerName, string fileName);
        Task DeleteAsync(string containerName, string fileName);

    }
}
