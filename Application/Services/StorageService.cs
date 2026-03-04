using Minio;
using Minio.DataModel.Args;
using Microsoft.Extensions.Configuration;

namespace Application.Services;

public class StorageService
{
    private readonly IMinioClient _minioClient;
    private readonly string _bucketName;

    public StorageService(IConfiguration config)
    {
        var minioConfig = config.GetSection("Minio");
        _bucketName = minioConfig["BucketName"];

        _minioClient = new MinioClient()
            .WithEndpoint(minioConfig["Endpoint"])
            .WithCredentials(minioConfig["AccessKey"], minioConfig["SecretKey"])
            .WithSSL(bool.Parse(minioConfig["UseSSL"]))
            .Build();
    }

    public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType)
    {
        // 1. Tạo Bucket nếu chưa có
        bool found = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(_bucketName));
        if (!found)
        {
            await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(_bucketName));
        }

        // 2. Upload file
        var putObjectArgs = new PutObjectArgs()
            .WithBucket(_bucketName)
            .WithObject(fileName)
            .WithStreamData(fileStream)
            .WithObjectSize(fileStream.Length)
            .WithContentType(contentType);

        await _minioClient.PutObjectAsync(putObjectArgs);

        // 3. Trả về tên file (để lưu vào DB)
        return fileName;
    }

    // Hàm lấy URL presigned (URL tạm thời để client tải trực tiếp)
    public async Task<string> GetFileUrlAsync(string fileName)
    {
        var args = new PresignedGetObjectArgs()
            .WithBucket(_bucketName)
            .WithObject(fileName)
            .WithExpiry(60 * 60 * 24); // Link sống 24h

        return await _minioClient.PresignedGetObjectAsync(args);
    }
}
