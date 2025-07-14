namespace Azureblob.Services;
using Azure.Storage.Blobs;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Storage.Blobs;
public class BlobStorageService
    {
        private readonly BlobContainerClient _containerClinet;
        private readonly IConfiguration _configuration;
        public BlobStorageService(IConfiguration configuration)
        {
            _configuration = configuration;
            var vaultUrl = configuration["AzureBlob:VaultUrl"];
            var client = new SecretClient(new Uri(vaultUrl), new DefaultAzureCredential());
            var sasUrl = client.GetSecret("containersasurl").Value.Value;
            _containerClinet = new BlobContainerClient(new Uri(sasUrl));
        }

        public async Task UploadFile(Stream fileStream,string fileName)
        {
            var blobClient = _containerClinet.GetBlobClient(fileName);
            await blobClient.UploadAsync(fileStream,overwrite:true);
        }

        public async Task<Stream> DownloadFile(string fileName)
        {
            var blobClient = _containerClinet?.GetBlobClient(fileName);
            if(await blobClient.ExistsAsync())
            {
                var downloadInfor = await blobClient.DownloadStreamingAsync();
                return downloadInfor.Value.Content;
            }
            return null;
        }
    }