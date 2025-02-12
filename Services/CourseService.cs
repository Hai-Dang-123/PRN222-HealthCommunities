using HealthCommunitiesCheck2.DTO;
using HealthCommunitiesCheck2.IService;
using HealthCommunitiesCheck2.UnitOfWork;
using HealthCommunitiesCheck2.Utilities;

namespace HealthCommunitiesCheck2.Services
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserUtility _userUtility;

        public CourseService(IUnitOfWork unitOfWork, UserUtility userUtility)
        {
            _unitOfWork = unitOfWork;
            _userUtility = userUtility;
        }
        
        public async Task<ResponseDTO> DeleteCourse(Guid courseId)
        {
            var course = _unitOfWork.Course.GetByGuid(courseId);
            if (course == null)
            {
                return new ResponseDTO("Course not found.", 404, false);
            }

            if (course.UserID != _userUtility.GetUserIDFromToken())
            {
                return new ResponseDTO("You do not have permission to delete this course.", 403, false);
            }

            course.IsDelete = true;
            await _unitOfWork.Course.UpdateAsync(course);
            

            return new ResponseDTO("Course deleted successfully.", 200, true);
        }

    }
}
