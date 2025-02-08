using HealthCommunitiesCheck2.Data;
using HealthCommunitiesCheck2.IRepositories;
using HealthCommunitiesCheck2.Model;

namespace HealthCommunitiesCheck2.Repositories
{
    public class VideoOfCourseRepository: GenericRepository<VideoOfCourse>, IVideoOfCourseRepository
    {
        private readonly ApplicationDbContext _context;
        public VideoOfCourseRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
