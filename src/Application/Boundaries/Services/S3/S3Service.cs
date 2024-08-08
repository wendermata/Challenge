using Amazon.S3;
using Amazon.S3.Model;
using Application.Boundaries.Services.S3.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Application.Boundaries.Services.S3
{
    public class S3Service : IS3Service
    {
        private readonly IAmazonS3 _client;
        private readonly S3Settings _settings;

        public S3Service(IOptions<S3Settings> options)
        {
            _settings = options.Value;
            _client = new AmazonS3Client(_settings.AccessKey, _settings.SecretKey, _settings.GetBucketRegion());
        }

        public async Task UploadFileAsync(IFormFile file, string keyName)
        {
            await using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0;

                PutObjectRequest request = new()
                {
                    InputStream = memoryStream,
                    BucketName = _settings.BucketName,
                    Key = keyName
                };
                await _client.PutObjectAsync(request);
            }
        }

        public async Task DeleteFileAsync(string keyName)
        {
            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = _settings.BucketName,
                Key = keyName
            };
            await _client.DeleteObjectAsync(deleteRequest);
        }

        public async Task ReplaceFileAsync(IFormFile file, string keyName)
        {

            await DeleteFileAsync(keyName);
            await UploadFileAsync(file, keyName);
        }

    }
}
