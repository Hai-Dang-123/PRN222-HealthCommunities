using HealthCommunitiesCheck2.Data;
using HealthCommunitiesCheck2.IRepositories;

using HealthCommunitiesCheck2.Repositories;

namespace HealthCommunitiesCheck2.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork (ApplicationDbContext context)
        {
            _context = context;
            User = new UserRepository(_context);
            Contact = new ContactRepository(_context);
            Course = new CourseRepository(_context);
            Enrollment = new EnrollmentRepository(_context);    
            News = new NewsRepository(_context);
            
            // ADD MORE
            Role = new RoleRepository(_context);
            Wallet = new WalletRepository(_context);
            Transaction = new TransactionRepository(_context);
            Token = new TokenRepository(_context);
            Reading = new ReadingOfCourseRepository(context);
            Video = new VideoOfCourseRepository(context);
            Google = new GoogleDriveRepository(context);
        }

        public IUserRepository User {  get; private set; }

        public IContactRepository Contact { get; private set; }

        public ICourseRepository Course { get; private set; }

        public IEnrollmentRepository Enrollment { get; private set; }

        public INewsRepository News { get; private set; }

        
        public IRoleRepository Role { get; private set; }
        public IWalletRepository Wallet { get; private set; }
        public ITransactionRepository Transaction { get; private set; }
        public ITokenRepository Token { get; private set; }
        public IReadingOfCourseRepository Reading { get; private set; }
        public IVideoOfCourseRepository Video { get; private set; }
        public IGoogleDriveRepository Google { get; private set; }
        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public async Task<bool> SaveChangeAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
