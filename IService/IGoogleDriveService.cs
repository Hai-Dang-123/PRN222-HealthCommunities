namespace HealthCommunitiesCheck2.IService
{
    public interface IGoogleDriveService
    {
        Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType);
    }
}
