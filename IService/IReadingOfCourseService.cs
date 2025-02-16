using HealthCommunitiesCheck2.DTO;

namespace HealthCommunitiesCheck2.IService
{
    public interface IReadingOfCourseService
    {
        Task<ResponseDTO> AddReadingAsync(ReadingDTO readingDto, Stream fileStream, string fileName, string contentType);
        Task<ResponseDTO> GetReadingsByCourseId(Guid courseId);
    }
}
