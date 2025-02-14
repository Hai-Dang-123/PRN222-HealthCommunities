using HealthCommunitiesCheck2.DTO;
using HealthCommunitiesCheck2.IService;
using HealthCommunitiesCheck2.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HealthCommunitiesCheck2.Pages
{
    public class CreateCourseModel : PageModel
    {
        private readonly ICourseService _courseService;

        public CreateCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }
        public void OnGet()
        {
        }

        [BindProperty]
        public CourseDTO NewCourse { get; set; } = new CourseDTO();

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


            return RedirectToPage("/ManageCourse");
        }
    }
}
