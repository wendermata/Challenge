using Microsoft.AspNetCore.Http;

namespace Application.Boundaries.Services.S3
{
    public interface IS3Service
    {
        Task UploadFileAsync(IFormFile file, string keyName);
        Task ReplaceFileAsync(IFormFile file, string keyName);
        Task DeleteFileAsync(string keyName);
    }
}
