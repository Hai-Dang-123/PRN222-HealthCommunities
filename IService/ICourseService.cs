using HealthCommunitiesCheck2.DTO;

namespace HealthCommunitiesCheck2.IService
{
    public interface ICourseService
    {
        Task<ResponseDTO> DeleteCourse(Guid courseId);
        Task<ResponseDTO> GetAllCourse();

        Task<ResponseDTO> AddNewCourse(CourseDTO courseDTO);

    }
}
