using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;

namespace ColorTesterAPI.Application;

public class GoogleCloudStorageService
{
    private readonly StorageClient _storageClient;
    private readonly string? _bucketName;

    public GoogleCloudStorageService(IConfiguration configuration)
    {
        _bucketName = configuration["GoogleCloudStorage:BucketName"];
        _storageClient = StorageClient.Create();
    }

    public string GetSignedUrl(string fileName, TimeSpan duration)
    {
        var urlSigner = UrlSigner.FromCredential(
            GoogleCredential.GetApplicationDefault().UnderlyingCredential as ServiceAccountCredential);
        var signedUrl = urlSigner.Sign(_bucketName, fileName, duration);
        return signedUrl;
    }
}