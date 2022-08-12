using Azure.Storage.Blobs;
using Entities.ConfigurationModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Service.Utils.Azure
{
    public class AzureBlobStorage : IAzureBlobStorage
    {
        private readonly string _storageConnectionString;
        private readonly string _storageContainerName;
        private readonly IConfiguration _configuration;
        private readonly AzureConfiguration _azureConfiguration;

        public AzureBlobStorage(IConfiguration configuration)
        {
            _configuration = configuration;
            _azureConfiguration = new AzureConfiguration();
            _configuration.Bind(_azureConfiguration.Section, _azureConfiguration);
            _storageConnectionString = _azureConfiguration.BlobConnectionString;
            _storageContainerName = _azureConfiguration.BlobContainerName;
        }

        public async Task<string> UploadImageAsync(IFormFile formFile)
        {
            string responseUrl;

            BlobContainerClient container = new BlobContainerClient(_storageConnectionString, _storageContainerName);

            try
            {
                BlobClient client = container.GetBlobClient(formFile.FileName);
                await using (Stream? data = formFile.OpenReadStream())
                {
                    await client.UploadAsync(data);
                }

                responseUrl = client.Uri.AbsoluteUri;
            } catch
            {
                throw new RestException(HttpStatusCode.InternalServerError, "Product Image Upload Failed");
            }

            return responseUrl;
        }
    }
}
