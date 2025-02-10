using HealthCommunitiesCheck2.Model;

namespace HealthCommunitiesCheck2.IRepositories
{
    public interface ITokenRepository : IGenericRepository<RefreshToken>
    {
        Task<RefreshToken> GetRefreshTokenByUserID(Guid userId);
        Task<RefreshToken?> GetRefreshTokenByKey(string refreshTokenKey);
    }
}
