using Azure;
using HealthCommunitiesCheck2.DTO;
using HealthCommunitiesCheck2.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HealthCommunitiesCheck2.Pages
{
    public class AddCourseContentModel : PageModel
    {
        private readonly IVideoOfCourseService _videoService;
        private readonly IReadingOfCourseService _readingService;
        private readonly ICourseService _courseService;

        public AddCourseContentModel(IVideoOfCourseService videoService, IReadingOfCourseService readingService, ICourseService courseService)
        {
            _videoService = videoService;
            _readingService = readingService;
            _courseService = courseService;
        }

        [BindProperty]
        public Guid CourseIdToUpload { get; set; }
        public string Message { get; set; }

        public List<VideoDTO> UploadedVideos { get; set; } = new List<VideoDTO>();
        public List<ReadingDTO> UploadedReadings { get; set; } = new List<ReadingDTO>();

        [BindProperty]
        public List<IFormFile> VideoFiles { get; set; } = new List<IFormFile>();

        [BindProperty]
        public List<IFormFile> ReadingFiles { get; set; } = new List<IFormFile>();

        // 🔹 Load lại CourseID từ session để tránh mất khi return Page()
        private void LoadCourseIdFromSession()
        {
            if (CourseIdToUpload == Guid.Empty)
            {
                var courseIdStr = HttpContext.Session.GetString("CourseID");
                if (!string.IsNullOrEmpty(courseIdStr))
                {
                    CourseIdToUpload = Guid.Parse(courseIdStr);
                    Console.WriteLine($"✅ Lấy lại CourseID từ session: {CourseIdToUpload}");
                }
                else
                {
                    Console.WriteLine("❌ Không tìm thấy CourseID trong session.");
                }
            }
        }

        public async Task OnGetAsync()
        {
            LoadCourseIdFromSession();
        }

        [RequestSizeLimit(500_000_000)] // 500MB
        public async Task<IActionResult> OnPostUploadVideoAsync()
        {
            LoadCourseIdFromSession(); // 🔹 Đảm bảo CourseIdToUpload không bị mất

            if (VideoFiles == null || VideoFiles.Count == 0)
            {
                Message = "❌ Vui lòng chọn ít nhất một video để upload.";
                return Page();
            }

            foreach (var video in VideoFiles)
            {
                var videoDto = new VideoDTO
                {
                    VideoID = Guid.NewGuid(),
                    CourseID = CourseIdToUpload,
                    Title = Path.GetFileNameWithoutExtension(video.FileName),
                    VideoFilePath = "" // Cập nhật sau khi upload thành công
                };

                using (var stream = video.OpenReadStream())
                {
                    var response = await _videoService.AddVideoAsync(videoDto, stream, video.FileName, video.ContentType);
                    if (!response.IsSuccess)
                    {
                        Message = "❌ Lỗi khi upload video: " + response.Message;
                        return Page();
                    }
                }
            }

            Message = "✅ Tải video thành công!";
            return Page();
        }

        public async Task<IActionResult> OnPostUploadReadingAsync()
        {
            LoadCourseIdFromSession(); // 🔹 Đảm bảo CourseIdToUpload không bị mất

            if (ReadingFiles == null || ReadingFiles.Count == 0)
            {
                Message = "❌ Vui lòng chọn ít nhất một tài liệu để upload.";
                return Page();
            }

            foreach (var reading in ReadingFiles)
            {
                var readingDto = new ReadingDTO
                {
                    ReadingID = Guid.NewGuid(),
                    CourseID = CourseIdToUpload,
                    Title = Path.GetFileNameWithoutExtension(reading.FileName),
                    FilePath = "" // Cập nhật sau khi upload thành công
                };

                using (var stream = reading.OpenReadStream())
                {
                    var response = await _readingService.AddReadingAsync(readingDto, stream, reading.FileName, reading.ContentType);
                    if (!response.IsSuccess)
                    {
                        Message = "❌ Lỗi khi upload tài liệu: " + response.Message;
                        return Page();
                    }
                }
            }

            Message = "✅ Tải tài liệu thành công!";
            return Page();
        }

        public async Task<IActionResult> OnPostCompleteCourseAsync()
        {
            LoadCourseIdFromSession(); // 🔹 Đảm bảo CourseIdToUpload không bị mất

            Console.WriteLine($"🔹 CourseIdToUpload trước khi gọi API: {CourseIdToUpload}");

            if (CourseIdToUpload == Guid.Empty)
            {
                Message = "❌ Lỗi: Course ID không hợp lệ.";
                return Page();
            }

            var response = await _courseService.UpdateCourseStatusAsync(CourseIdToUpload, "Completed");
            if (!response.IsSuccess)
            {
                Message = "❌ Lỗi khi hoàn thành khóa học: " + response.Message;
                return Page();
            }

            Message = "✅ Khóa học đã được hoàn thành!";
            return RedirectToPage("/ManageCourse");
        }
    }
}
