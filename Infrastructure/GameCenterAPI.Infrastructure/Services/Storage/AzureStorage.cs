using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using GameCenterAPI.Application.Abstraction.Storage;
using GameCenterAPI.Infrastructure.Operations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace GameCenterAPI.Infrastructure.Services.Storage
{
    public class AzureStorage : IAzureStorage
    {
        readonly BlobServiceClient _blobServiceClient;
        BlobContainerClient _blobContainerClient;

        public AzureStorage(IConfiguration configuration)
        {
            _blobServiceClient = new(configuration["Storage:Azure"]);
        }

        public async Task DeleteAsync(string containerName, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
            await blobClient.DeleteAsync();
        }

        public List<string> GetFiles(string containerName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            return _blobContainerClient.GetBlobs().Select(b => b.Name).ToList();
        }

        public bool HasFile(string containerName, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            return _blobContainerClient.GetBlobs().Any(b => b.Name == fileName);
        }

        public async Task<List<(string fileName, string containerName)>> UploadAsync(string containerName, IFormFileCollection files)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            //await _blobContainerClient.CreateIfNotExistsAsync();
            await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);

            List<(string fileName, string containerName)> datas = new();

            foreach (IFormFile file in files)
            {
                string fileName = await FileRenameAsync(containerName, file.FileName, true);
                BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
                await blobClient.UploadAsync(file.OpenReadStream());
                datas.Add((fileName, containerName));
            }

            return datas;
        }

        private async Task<string> FileRenameAsync(string containerName, string fileName, bool first = true)
        {
            string newFileName = await Task.Run<string>(async () =>
            {
                string extension = Path.GetExtension(fileName);
                string newFileName = string.Empty;

                if (first)
                {
                    string oldName = Path.GetFileNameWithoutExtension(fileName);
                    newFileName = $"{NameOperation.CharacterRegulatory(oldName)}{extension}";
                }
                else
                {
                    newFileName = fileName;
                    int indexNo1 = newFileName.IndexOf("-");
                    if (indexNo1 == -1)
                        newFileName = $"{Path.GetFileNameWithoutExtension(newFileName)}-2{extension}";
                    else
                    {
                        int lastIndex = 0;
                        while (true)
                        {
                            lastIndex = indexNo1;
                            indexNo1 = newFileName.IndexOf("-", indexNo1 + 1);
                            if (indexNo1 == -1)
                            {
                                indexNo1 = lastIndex;
                                break;
                            }
                        }

                        int indexNo2 = newFileName.IndexOf(".");
                        string fileNo = newFileName.Substring(indexNo1 + 1, indexNo2 - indexNo1 - 1);

                        if (int.TryParse(fileNo, out int _fileNo))
                        {
                            _fileNo++;
                            newFileName = newFileName.Remove(indexNo1 + 1, indexNo2 - indexNo1 - 1).Insert(indexNo1, _fileNo.ToString());
                        }

                        return newFileName;
                    }
                }

                if (HasFile(containerName, newFileName))
                {
                    return await FileRenameAsync(containerName, newFileName, false);
                }
                else
                    return newFileName;
            });

            return newFileName;
        }
    }
}
