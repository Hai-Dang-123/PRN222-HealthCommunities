using HealthCommunitiesCheck2.Data;
using HealthCommunitiesCheck2.IRepositories;
using HealthCommunitiesCheck2.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HealthCommunitiesCheck2.Repositories
{
    public class EnrollmentRepository : GenericRepository<Enrollment>, IEnrollmentRepository
    {
        private readonly ApplicationDbContext _context;
    public EnrollmentRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

        public async Task<IEnumerable<Enrollment>> FindAsync(Expression<Func<Enrollment, bool>> predicate)
        {
            return await _context.Enrollments
                .Where(predicate)
                .Include(e => e.Course)  // Load Course luôn
                .ToListAsync();
        }
    }
}
