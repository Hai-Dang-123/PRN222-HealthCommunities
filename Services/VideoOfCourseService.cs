using HealthCommunitiesCheck2.DTO;
using HealthCommunitiesCheck2.IRepositories;
using HealthCommunitiesCheck2.IService;
using HealthCommunitiesCheck2.Model;
using HealthCommunitiesCheck2.UnitOfWork;

namespace HealthCommunitiesCheck2.Services
{
    public class VideoOfCourseService : IVideoOfCourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGoogleDriveService _googleDriveService;

        public VideoOfCourseService (IUnitOfWork unitOfWork, IGoogleDriveService googleDriveService)
        {
            _unitOfWork = unitOfWork;
            _googleDriveService = googleDriveService;
        }
        public async Task<ResponseDTO> AddVideoAsync(VideoDTO videoDto, Stream fileStream, string fileName, string contentType)
        {
            try
            {
                string fileId = await _googleDriveService.UploadFileAsync(fileStream, fileName, contentType);
                videoDto.VideoFilePath = $"https://drive.google.com/uc?id={fileId}";

                var video = new VideoOfCourse
                {
                    VideoID = videoDto.VideoID,
                    CourseID = videoDto.CourseID,
                    Title = videoDto.Title,
                    VideoFilePath = videoDto.VideoFilePath,
                    Duration = videoDto.Duration,
                    CreatedAt = videoDto.CreatedAt
                };

                await _unitOfWork.Video.AddAsync(video);
                // 🔥 Lưu vào bảng UploadedFile
                var uploadedFile = new UploadedFile
                {
                    FileID = Guid.NewGuid(),
                    Title = videoDto.Title,
                    GoogleDriveFileID = fileId,
                    FileType = "video", // Đây là video
                    CreatedAt = DateTime.UtcNow
                };

                await _unitOfWork.GoogleDrive.AddAsync(uploadedFile);
                await _unitOfWork.SaveChangeAsync();
                return new ResponseDTO("Video uploaded successfully", 200, true, videoDto);
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Failed to upload video: {ex.Message}", 500, false);
            }
        }

        public async Task<ResponseDTO> GetVideosByCourseId(Guid courseId)
        {
            var videos = await _unitOfWork.Video.GetVideosByCourseId(courseId);
            var result = videos.Select(v => new VideoDTO
            {
                VideoID = v.VideoID,
                CourseID = v.CourseID,
                Title = v.Title,
                VideoFilePath = v.VideoFilePath,
                Duration = v.Duration,
                CreatedAt = v.CreatedAt
            }).ToList();

            return new ResponseDTO("Videos retrieved successfully", 200, true, result);
        }
    }
    }

