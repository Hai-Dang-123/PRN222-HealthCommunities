using HealthCommunitiesCheck2.DTO;

namespace HealthCommunitiesCheck2.IService
{
    public interface IVideoOfCourseService
    {
        Task<ResponseDTO> AddVideoAsync(VideoDTO videoDto, Stream fileStream, string fileName, string contentType);
        Task<ResponseDTO> GetVideosByCourseId(Guid courseId);
    }
}
