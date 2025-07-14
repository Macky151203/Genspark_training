public class BlobStorageService{
    private readonly BlobServiceClient _containerClient;
    public BlobStorageService(IConfiguration configuration)
    {
        var sasUrl = configuration["AzureBlob:ContainerSasUrl"];
        _containerClient = new BlobServiceClient(new Uri(sasUrl));
    }

    
}