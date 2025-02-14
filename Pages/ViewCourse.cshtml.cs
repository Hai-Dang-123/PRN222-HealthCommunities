using HealthCommunitiesCheck2.DTO;
using HealthCommunitiesCheck2.IService;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

public class ViewCourseModel : PageModel
{
    private readonly ICourseService _courseService;

    public ViewCourseModel(ICourseService courseService)
    {
        _courseService = courseService;
    }

    public List<CourseDTO> Courses { get; set; }
    public List<EnrollmentDTO> EnrolledCourses { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        // Lấy danh sách khóa học
        var response = await _courseService.GetAllCourse();
        if (response.StatusCode == 200 && response.Result != null)
        {
            if (response.Result is List<CourseDTO> coursesList)
            {
                Courses = coursesList;
            }
            else
            {
                Console.WriteLine("Dữ liệu trả về không phải là List<CourseDTO>");
                Courses = new List<CourseDTO>();
            }
        }
        else
        {
            Console.WriteLine("Không lấy được dữ liệu khóa học");
            Courses = new List<CourseDTO>();
        }

        // Lấy danh sách khóa học đã đăng ký
        var enrolledResponse = await _courseService.GetEnrolledCourses();
        if (enrolledResponse.StatusCode == 200 && enrolledResponse.Result != null)
        {
            if (enrolledResponse.Result is List<EnrollmentDTO> enrolledCoursesList)
            {
                EnrolledCourses = enrolledCoursesList;
            }
            else
            {
                Console.WriteLine("Dữ liệu trả về không phải là List<EnrollmentDTO>");
                EnrolledCourses = new List<EnrollmentDTO>();
            }
        }
        else
        {
            EnrolledCourses = new List<EnrollmentDTO>();
            Console.WriteLine("Không lấy được danh sách khóa học đã đăng ký");
        }

        return Page();
    }

    public async Task<IActionResult> OnPostDeleteAsync(Guid courseId)
    {
        var response = await _courseService.DeleteCourse(courseId);
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostAddCourseAsync(CourseDTO courseDTO)
    {
        var response = await _courseService.AddNewCourse(courseDTO);
        return RedirectToPage();
    }
}