using HealthCommunitiesCheck2.Model;

namespace HealthCommunitiesCheck2.IRepositories
{
    public interface IReadingOfCourseRepository : IGenericRepository<ReadingOfCourse>
    {
        Task<List<ReadingOfCourse>> GetReadingsByCourseId(Guid courseId);
    }
}
