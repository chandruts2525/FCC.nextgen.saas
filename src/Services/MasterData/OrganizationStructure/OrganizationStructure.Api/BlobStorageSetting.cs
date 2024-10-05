using FCC.Core.Constants;
using Softura.Azure.Storage.Blobs.Abstractions;

namespace OrganizationManagementService.Api
{
	public class BlobStorageSetting: IBlobStorageSetting
    {
        private readonly IConfiguration _configuration;
        public BlobStorageSetting(IConfiguration configuration) {
            _configuration = configuration;
        }
        public string ConnectionString
        {
            get
            {
				return _configuration[Keys.AzureBlobStorageKey];
            }
            set => throw new NotImplementedException();
        }
    }
}
