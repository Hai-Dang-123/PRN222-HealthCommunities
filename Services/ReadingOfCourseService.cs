using HealthCommunitiesCheck2.DTO;
using HealthCommunitiesCheck2.IService;
using HealthCommunitiesCheck2.Model;
using HealthCommunitiesCheck2.UnitOfWork;

namespace HealthCommunitiesCheck2.Services
{
    public class ReadingOfCourseService : IReadingOfCourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGoogleDriveService _googleDriveService;

        public ReadingOfCourseService (IUnitOfWork unitOfWork, IGoogleDriveService googleDriveService)
        {
            _unitOfWork = unitOfWork;
            _googleDriveService = googleDriveService;
        }
        public async Task<ResponseDTO> AddReadingAsync(ReadingDTO readingDto, Stream fileStream, string fileName, string contentType)
        {
            try
            {
                string fileId = await _googleDriveService.UploadFileAsync(fileStream, fileName, contentType);
                readingDto.FilePath = $"https://drive.google.com/uc?id={fileId}";

                var reading = new ReadingOfCourse
                {
                    ReadingID = readingDto.ReadingID,
                    CourseID = readingDto.CourseID,
                    Title = readingDto.Title,
                    FilePath = readingDto.FilePath,
                    CreatedAt = readingDto.CreatedAt
                };

                await _unitOfWork.Reading.AddAsync(reading);
                // 🔥 Lưu vào bảng UploadedFile
                var uploadedFile = new UploadedFile
                {
                    FileID = Guid.NewGuid(),
                    Title = readingDto.Title,
                    GoogleDriveFileID = fileId,
                    FileType = "reading", // Đây là tài liệu đọc
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.GoogleDrive.AddAsync(uploadedFile);
                await _unitOfWork.SaveChangeAsync();
                return new ResponseDTO("Reading uploaded successfully", 200, true, readingDto);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Failed to upload reading: {ex.Message}", 500, false);
            }
        }

        public async Task<ResponseDTO> GetReadingsByCourseId(Guid courseId)
        {
            var readings = await _unitOfWork.Reading.GetReadingsByCourseId(courseId);
            var result = readings.Select(r => new ReadingDTO
            {
                ReadingID = r.ReadingID,
                CourseID = r.CourseID,
                Title = r.Title,
                FilePath = r.FilePath,
                CreatedAt = r.CreatedAt
            }).ToList();

            return new ResponseDTO("Readings retrieved successfully", 200, true, result);
        }
    }
}
