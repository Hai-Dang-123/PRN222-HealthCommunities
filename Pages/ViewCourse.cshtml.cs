using HealthCommunitiesCheck2.DTO;
using HealthCommunitiesCheck2.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace HealthCommunitiesCheck2.Pages
{
    public class ViewCourseModel : PageModel
    {
        private readonly ICourseService _courseService;
        public ViewCourseModel(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public List<CourseDTO> Courses { get; set; }

        public async Task OnGetAsync()
        {
            var response = await _courseService.GetAllCourse();

            if (response != null && response.IsSuccess && response.Result != null)
            {
                if (response.Result is List<CourseDTO> courseList)
                {
                    Courses = courseList;
                }
                else
                {
                    Courses = JsonConvert.DeserializeObject<List<CourseDTO>>(JsonConvert.SerializeObject(response.Result));
                }
            }
            else
            {
                Courses = new List<CourseDTO>();
            }
        }

    }
}
