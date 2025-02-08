using HealthCommunitiesCheck2.Data;
using HealthCommunitiesCheck2.IRepositories;
using HealthCommunitiesCheck2.Model;
using Microsoft.EntityFrameworkCore;

namespace HealthCommunitiesCheck2.Repositories
{
    public class ReadingOfCourseRepository : GenericRepository<ReadingOfCourse>, IReadingOfCourseRepository
    {
        private readonly ApplicationDbContext _context;
    public ReadingOfCourseRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}
}
