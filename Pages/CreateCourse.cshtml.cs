using HealthCommunitiesCheck2.DTO;
using HealthCommunitiesCheck2.IService;
using HealthCommunitiesCheck2.Model;
using HealthCommunitiesCheck2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;

namespace HealthCommunitiesCheck2.Pages
{
    public class CreateCourseModel : PageModel
    {
        private readonly ICourseService _courseService;
        //
        private readonly IVideoOfCourseService _videoService;
        private readonly IReadingOfCourseService _readingService;
        private readonly IGoogleDriveService _googleDriveService;

        public CreateCourseModel(ICourseService courseService, IVideoOfCourseService videoService, IReadingOfCourseService readingService, IGoogleDriveService googleDriveService)
        {
            _courseService = courseService;
            _videoService = videoService;
            _readingService = readingService;
        }
        public void OnGet()
        {
        }

        [BindProperty]
        public CourseDTO NewCourse { get; set; } = new CourseDTO();

        [BindProperty]
        public List<IFormFile> UploadedVideos { get; set; } = new List<IFormFile>();

        [BindProperty]
        public List<IFormFile> UploadedReadings { get; set; } = new List<IFormFile>();

        public async Task<IActionResult> OnPostCreateAsync()
        {
            
            if (!ModelState.IsValid)
            {

                return Page();
            }

           
            var response = await _courseService.AddNewCourse(NewCourse);
            if (!response.IsSuccess)
            {
                return Page();
            }

            var createdCourse = response.Result as Course;
            Console.WriteLine($"Redirecting to AddCourseContent with CourseID: {createdCourse.CourseID}");

            //var url = Url.Page("/AddCourseContent", new { courseId = createdCourse.CourseID });
            //Console.WriteLine($"Redirecting to: {url}");
            var courseId = createdCourse.CourseID;
            //return RedirectToPage("/AddCourseContent", new { courseId = createdCourse.CourseID });
            HttpContext.Session.SetString("CourseID", courseId.ToString());

            //// Đọc lại từ Session
            //var storedCourseId = HttpContext.Session.GetString("CourseID");

            //// In ra Console để kiểm tra
            //Console.WriteLine($"CourseID Stored in Session: {storedCourseId}");


            return RedirectToPage("/AddCourseContent");

        }
    }
}
