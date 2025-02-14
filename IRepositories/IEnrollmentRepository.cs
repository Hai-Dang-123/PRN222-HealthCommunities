using HealthCommunitiesCheck2.Model;
using System.Linq.Expressions;

namespace HealthCommunitiesCheck2.IRepositories
{
    public interface IEnrollmentRepository : IGenericRepository<Enrollment>
    {
        Task<IEnumerable<Enrollment>> FindAsync(Expression<Func<Enrollment, bool>> predicate);
    }
}
