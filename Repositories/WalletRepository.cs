using HealthCommunitiesCheck2.Data;
using HealthCommunitiesCheck2.IRepositories;
using HealthCommunitiesCheck2.Model;

namespace HealthCommunitiesCheck2.Repositories
{
    public class WalletRepository : GenericRepository<Wallet>, IWalletRepository
    {
        private readonly ApplicationDbContext _context;
        public WalletRepository (ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
