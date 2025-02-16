using HealthCommunitiesCheck2.IRepositories;
using HealthCommunitiesCheck2.IService;

namespace HealthCommunitiesCheck2.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository User {  get; }
        // Add more
        IContactRepository Contact { get; }
        ICourseRepository Course { get; }
        IEnrollmentRepository Enrollment { get; }
        INewsRepository News { get; }
        
        IRoleRepository Role { get; }
        IWalletRepository Wallet { get; }
        ITransactionRepository Transaction { get; }
        ITokenRepository Token { get; }
        IVideoOfCourseRepository Video { get; }
        IReadingOfCourseRepository Reading {  get; }
        IGoogleDriveRepository GoogleDrive { get; }
        Task<int> SaveAsync();
        Task<bool> SaveChangeAsync();
    }
}
