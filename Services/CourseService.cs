using HealthCommunitiesCheck2.DTO;
using HealthCommunitiesCheck2.IService;
using HealthCommunitiesCheck2.Model;
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

        public Task<ResponseDTO> Create(Guid courseId)
        {
            throw new NotImplementedException();
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

        public async Task<ResponseDTO> GetAllCourse()
        {

            try
            {
                var course = await _unitOfWork.Course.ToListAsync();
                return new ResponseDTO("Courses retrieved successfully", 200, true, course);


            }
            catch (Exception ex)
            {

                return new ResponseDTO("Courses retrieved false", 400, false);
            }

        }
        public async Task<ResponseDTO> AddNewCourse(CourseDTO courseDTO)
        {
            var userId = _userUtility.GetUserIDFromToken();
            if (userId == Guid.Empty)
                return new ResponseDTO("Unauthorized", 401, false);
            var user = await _unitOfWork.User.GetByIdAsync(userId);
            if (user == null)
                return new ResponseDTO("User not found", 404, false);
            if (string.IsNullOrWhiteSpace(courseDTO.Title))
                return new ResponseDTO("Course name can't be blank", 400, false);
            if (user != null && user.Role != null && user.Role.Equals("Student"))
            {
                return new ResponseDTO("You not allow to do this", 444, false);
            }

            var course = new Course
            {
                UserID = userId,
                CourseID = Guid.NewGuid(),
                Title = courseDTO.Title,
                Description = courseDTO.Description,
                StartDate = DateTime.Now,
                Price = courseDTO.Price,
                EndDate = courseDTO.EndDate,
                IsOnline = courseDTO.IsOnline,
                CreatedAt = courseDTO.CreatedAt,
                Status = courseDTO.Status,
            };
            await _unitOfWork.Course.AddAsync(course);
            await _unitOfWork.SaveChangeAsync();

            return new ResponseDTO("Add course successfull", 200,
                true, course);

        }
        public async Task<ResponseDTO> UpdateCourseStatusAsync(Guid courseId, string status)
        {
            var course = await _unitOfWork.Course.GetByIdAsync(courseId);
            if (course == null)
                return new ResponseDTO("Khóa học không tồn tại", 404, false);

            course.Status = status;
            await _unitOfWork.Course.UpdateAsync(course);
            await _unitOfWork.SaveChangeAsync();

            return new ResponseDTO("Cập nhật trạng thái thành công", 200, true);
        }
    }
}
