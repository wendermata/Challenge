using Amazon;

namespace Application.Boundaries.Services.S3.Settings
{
    public class S3Settings : IS3Settings
    {
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string BucketName { get; set; }
        public string FilePath { get; set; }
        public RegionEndpoint GetBucketRegion() => RegionEndpoint.USEast2;
    };

    public interface IS3Settings
    {
        string AccessKey { get; set; }
        string SecretKey { get; set; }
        string BucketName { get; set; }
        string FilePath { get; set; }
        RegionEndpoint GetBucketRegion();
    };
}
