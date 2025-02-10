using HealthCommunitiesCheck2.Model;

namespace HealthCommunitiesCheck2.IRepositories
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        Task<Role> GetByIdAsync(Guid roleId);
        Task<List<Role>> GetAllAsync();
    }
}
