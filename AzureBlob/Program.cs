using Azure;
using AzureLibrary.AzureBlobStorage.BlobStorage;
using Microsoft.AspNetCore.Http;

namespace AzurBlob
{
    public class program
    {
        public static async Task Main(string[] args)
        {
             #region UploadFle
             try
             {
                 AzureBlobStoragecontainer azureBlobServices = new AzureBlobStoragecontainer("DefaultEndpointsProtocol=https;AccountName=blobpdfstorage;AccountKey=2sgJCBMQi594LZ9DCEpQN99u2yzi1SMfwr9HueKM5a8EWdOUgxbCa/WajaEUrynq2vsART7yE+Xp+ASt0cc8gg==;EndpointSuffix=core.windows.net", "ua2-jp-marubeni");

                 List<string> filePaths = new List<string>
                 {
                     @"C:\Users\visualapp\Downloads\Practical MCA.pdf",
                     @"C:\Users\visualapp\Downloads\My Active Contacts 10-10-2023 10-50-36 AM.xlsx"
                 };

                var uploadResponses = await azureBlobServices.AzureBlobUploadFile(filePaths);
                 if (uploadResponses.All(response => response.Success))
                 {
                     Console.WriteLine("Success");
                 }
                 else
                 {
                     Console.WriteLine("Some uploads failed. Failed files:");
                     foreach (var response in uploadResponses.Where(r => !r.Success))
                     {
                         Console.WriteLine(response.FileName);
                     }
                 }
             }
             catch (RequestFailedException ex)
             {
                 Console.WriteLine(ex.Message);
             }
             catch (Exception ex)
             {
                 Console.WriteLine($"An error occurred: {ex.Message}");
             }

             #endregion       

            AzureBlobStoragecontainer azureBlobService = new AzureBlobStoragecontainer("DefaultEndpointsProtocol=https;AccountName=blobpdfstorage;AccountKey=2sgJCBMQi594LZ9DCEpQN99u2yzi1SMfwr9HueKM5a8EWdOUgxbCa/WajaEUrynq2vsART7yE+Xp+ASt0cc8gg==;EndpointSuffix=core.windows.net", "uploadfile");
            string blobName = "001/tinywow_write_paragraph_writer_35116740.txt";

            try
            {
                byte[] blobData = await azureBlobService.ReadBlobFileAsync(blobName);
                if (blobData != null)
                {
                    File.WriteAllBytes("E:\\output.txt", blobData);
                    Console.WriteLine("Blob data retrieved successfully.");
                }
                else
                {
                    Console.WriteLine("Blob not found.");
                }
            }
            catch(FileNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}