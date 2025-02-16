using HealthCommunitiesCheck2.Model;

namespace HealthCommunitiesCheck2.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> FindByEmailAsync(string email);
    }
}
