using HealthCommunitiesCheck2.DTO;

namespace HealthCommunitiesCheck2.IService
{
    public interface ICourseService
    {
        Task<ResponseDTO> DeleteCourse(Guid courseId);
        Task<ResponseDTO> Create(Guid courseId);

    }
}
