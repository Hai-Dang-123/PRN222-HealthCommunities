using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using HealthCommunitiesCheck2.IService;

namespace HealthCommunitiesCheck2.Services
{
    public class GoogleDriveService : IGoogleDriveService
    {
        private readonly DriveService _service;
        private const string FolderId = "1Ppl0nYKoe6JTY94cMuBXWIQbhaTfntrR"; // ID thư mục Google Drive

        public GoogleDriveService()
        {
            using var stream = new FileStream("wwwroot/credentials/healthcommunities-prn222-f6c0efc06349.json", FileMode.Open, FileAccess.Read);
            var credential = GoogleCredential.FromStream(stream).CreateScoped(DriveService.ScopeConstants.DriveFile);

            _service = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "HealthCommunities"
            });
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType)
        {
            var fileMetadata = new Google.Apis.Drive.v3.Data.File
            {
                Name = fileName,
                Parents = new List<string> { FolderId }
            };

            var request = _service.Files.Create(fileMetadata, fileStream, contentType);
            request.Fields = "id";
            await request.UploadAsync();

            return request.ResponseBody.Id; // Trả về Google Drive File ID
        }
    }
}
