using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Azure.Storage;
using Microsoft.Extensions.Configuration;
using Softura.Core.Extensions;
using Azure.Storage.Blobs.Specialized;
using FCC.Core.Constants;
using Microsoft.Extensions.Logging;

namespace FCC.Core
{
    public class BlobUrlSasToken
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        public BlobUrlSasToken(IConfiguration configuration,ILogger logger) 
        {
            _configuration = configuration;
            _logger = logger;
        }

        public Task<string> GetBlobURLSASToken(string fileName)
        {
            string BlobSasUri = string.Empty;
            try
            {
                string connectionString = _configuration[Keys.AzureBlobStorageKey];//_configuration.GetValue<string>(StaticKeys.AzureBlobConnectionstringKey);
                string BlobContainerName = _configuration.GetValue<string>("BlobContainerName");
                string AccountName = "fccfmsstoragedev";
                string AccountKey = connectionString.GetAccountKey();
                string TimeinSeconds = _configuration.GetValue<string>("AzureBlobExpireTimeSec");

                var blobServiceClient = new BlobServiceClient(connectionString);
                var blobContainerClient = blobServiceClient.GetBlobContainerClient(BlobContainerName);
                var storageSharedKeyCredential = new StorageSharedKeyCredential(AccountName, AccountKey);
                int Time = Int16.Parse(TimeinSeconds);
                BlobSasUri = GetBlobSasUri(blobContainerClient, fileName, storageSharedKeyCredential, Time);
            }
            catch(Exception)
            {
                _logger.LogError("Error in creating GetBlobURLSASToken");
            }

            return Task.FromResult(BlobSasUri);
        }


        private string GetBlobSasUri(BlobContainerClient container, string blobName, StorageSharedKeyCredential key, int TimeStamp = 0, string storedPolicyName = null)
        {
            // Create a SAS token that's valid for one min.
            BlobSasBuilder sasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = container.Name,
                BlobName = blobName,
                Resource = "b",
            };

            if (storedPolicyName == null)
            {
                sasBuilder.StartsOn = DateTimeOffset.UtcNow;
                sasBuilder.ExpiresOn = DateTimeOffset.UtcNow.AddSeconds(TimeStamp);
                sasBuilder.SetPermissions(BlobContainerSasPermissions.Read);
            }
            else
            {
                sasBuilder.Identifier = storedPolicyName;
            }

            // Use the key to get the SAS token.
            string sasToken = sasBuilder.ToSasQueryParameters(key).ToString();

            return container.GetBlockBlobClient(blobName).Uri + "?" + sasToken;
        }
    }
}
