using HealthCommunitiesCheck2.Data;
using HealthCommunitiesCheck2.IRepositories;
using HealthCommunitiesCheck2.Model;

namespace HealthCommunitiesCheck2.Repositories
{
    public class GoogleDriveRepository : GenericRepository<UploadedFile>, IGoogleDriveRepository
    {
        private readonly ApplicationDbContext _context;
        public GoogleDriveRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }


    }
    
    }

