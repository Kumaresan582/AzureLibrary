using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureLibrary.AzureBlobStorage.Model
{
    public class BlobUploadResponse
    {
        public string FileName { get; set; }
        public string Uri { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
