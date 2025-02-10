using HealthCommunitiesCheck2.Data;
using HealthCommunitiesCheck2.IRepositories;
using HealthCommunitiesCheck2.Model;
using Microsoft.EntityFrameworkCore;

namespace HealthCommunitiesCheck2.Repositories
{
    public class TokenRepository : GenericRepository<RefreshToken>, ITokenRepository
    {
        private readonly ApplicationDbContext _context;
        public TokenRepository (ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<RefreshToken> GetRefreshTokenByUserID(Guid userId)
        {
            // lấy token đúng id và chưa bị thu hồi
            return await _context.RefreshTokens
                .Where(rt => rt.UserId == userId && !rt.IsRevoked)
                .FirstOrDefaultAsync();
        }
        public async Task<RefreshToken?> GetRefreshTokenByKey(string refreshTokenKey)
        {
            if (string.IsNullOrWhiteSpace(refreshTokenKey))
            {
                throw new ArgumentException("Refresh token cannot be null or empty.", nameof(refreshTokenKey));
            }

            // Thực hiện truy vấn để tìm RefreshToken theo RefreshTokenKey
            var refreshTokenEntity = await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.RefreshTokenKey == refreshTokenKey);

            return refreshTokenEntity;
        }
    }
}
