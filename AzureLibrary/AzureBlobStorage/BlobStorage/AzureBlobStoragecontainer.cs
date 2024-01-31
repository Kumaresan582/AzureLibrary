using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using AzureLibrary.AzureBlobStorage.Model;
using Microsoft.AspNetCore.Http;

namespace AzureLibrary.AzureBlobStorage.BlobStorage
{
    public class AzureBlobStoragecontainer
    {
        private readonly BlobContainerClient _blobContainer;

        public AzureBlobStoragecontainer(string connectionString, string containerName)
        {
            _blobContainer = GetBlobContainer(connectionString, containerName);
        }

        private BlobContainerClient GetBlobContainer(string connectionString, string containerName)
        {
            try
            {
                var blobServiceClient = new BlobServiceClient(connectionString);
                var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);

                if (!blobContainerClient.Exists())
                {
                    blobContainerClient.Create();
                }

                return blobContainerClient;
            }
            catch (RequestFailedException ex)
            {
                throw new Exception($"Failed to get or create the container: {ex.Message}", ex);
            }
        }

        public Task<List<BlobUploadResponse>> AzureBlobUploadFile(string filePath)
        {
            return UploadFiles(null, null, filePath);
        }

        public Task<List<BlobUploadResponse>> AzureBlobUploadFile(List<string> filePaths)
        {
            return UploadFiles(filePaths, null, null);
        }

        public Task<List<BlobUploadResponse>> AzureBlobUploadFile(List<IFormFile> formFiles)
        {
            return UploadFiles(null, formFiles, null);
        }

        public async Task<List<BlobUploadResponse>> UploadFiles(List<string> filePaths = null, List<IFormFile> formFiles = null, string singleFilePath = null)
        {
            var blobUploadResponses = new List<BlobUploadResponse>();

            async Task UploadFileAsync(string fileName, Stream stream)
            {
                try
                {
                    var client = await _blobContainer.UploadBlobAsync(fileName, stream, default);

                    blobUploadResponses.Add(new BlobUploadResponse
                    {
                        FileName = fileName,
                        Uri = client.Value.ToString(),
                        Success = true,
                        ErrorMessage = null
                    });
                }
                catch (Exception ex)
                {
                    blobUploadResponses.Add(new BlobUploadResponse
                    {
                        FileName = fileName,
                        Uri = null,
                        Success = false,
                        ErrorMessage = ex.Message
                    });
                }
            }

            if (filePaths != null && filePaths.Any())
            {
                foreach (var filePath in filePaths)
                {
                    using (var fileStream = File.OpenRead(filePath))
                    {
                        await UploadFileAsync(Path.GetFileName(filePath), fileStream);
                    }
                }
            }
            else if (formFiles != null && formFiles.Any())
            {
                foreach (var formFile in formFiles)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        formFile.CopyTo(memoryStream);
                        memoryStream.Position = 0;

                        await UploadFileAsync(formFile.FileName, memoryStream);
                    }
                }
            }
            else if (!string.IsNullOrEmpty(singleFilePath))
            {
                using (var fileStream = File.OpenRead(singleFilePath))
                {
                    await UploadFileAsync(Path.GetFileName(singleFilePath), fileStream);
                }
            }

            return blobUploadResponses;
        }

        public async Task<byte[]> ReadBlobFileAsync(string blobName)
        {
            try
            {
                BlobClient blobClient = _blobContainer.GetBlobClient(blobName);

                if (await blobClient.ExistsAsync())
                {
                    BlobDownloadInfo blobDownloadInfo = await blobClient.DownloadAsync();

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        await blobDownloadInfo.Content.CopyToAsync(memoryStream);
                        return memoryStream.ToArray();
                    }
                }

                throw new FileNotFoundException($"Blob file not found: {blobName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading blob file: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> DeleteBlobFileAsync(string blobName)
        {
            try
            {
                BlobClient blobClient = _blobContainer.GetBlobClient(blobName);

                if (await blobClient.ExistsAsync())
                {
                    await blobClient.DeleteAsync();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting blob file: {ex.Message}");
                return false;
            }
        }
    }
}