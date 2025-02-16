using HealthCommunitiesCheck2.Data;
using HealthCommunitiesCheck2.IRepositories;
using HealthCommunitiesCheck2.Model;
using Microsoft.EntityFrameworkCore;

namespace HealthCommunitiesCheck2.Repositories
{
    public class NewsRepository : GenericRepository<News>, INewsRepository
    {
        private readonly ApplicationDbContext _context;
    public NewsRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}
}
