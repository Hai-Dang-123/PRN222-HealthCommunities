using HealthCommunitiesCheck2.Auth;

namespace HealthCommunitiesCheck2.Utilities
{
    public class UserUtility
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserUtility(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        //public Guid GetUserIdFromToken()
        //{
        //    var userIdClaim = _httpContextAccessor.HttpContext?.User.Claims.FirstOrDefault(c => c.Type == "userId" || c.Type == "sub");
        //    if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
        //    {
        //        return userId;
        //    }
        //    return Guid.Empty;
        //}
        public string? GetFullNameFromToken()
        {
            try
            {
                var token = _httpContextAccessor.HttpContext?.Request.Cookies["AccessToken"];
                if (string.IsNullOrEmpty(token))
                    return ""; // Không có token => Trả về mặc định

                var claims = JwtProvider.DecodeToken(token);
                var fullNameClaim = claims.FirstOrDefault(c => c.Type == "FullName");

                return fullNameClaim?.Value; // Nếu không có FullName, trả về "User"
            }
            catch
            {
                return ""; // Nếu lỗi, trả về mặc định
            }

        }
        // ✅ Lấy UserID từ Token
        public Guid GetUserIDFromToken()
        {
            try
            {
                var token = _httpContextAccessor.HttpContext?.Request.Cookies["AccessToken"];
                if (string.IsNullOrEmpty(token))
                    return Guid.Empty; 

                var claims = JwtProvider.DecodeToken(token);
                var userIdClaim = claims.FirstOrDefault(c => c.Type == "UserId" || c.Type == "sub");

                if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
                {
                    return userId;
                }

                return Guid.Empty; 
            }
            catch
            {
                return Guid.Empty; 
            }
        }
        // lấy role từ token
        public string? GetRoleFromToken()
        {

            try
            {
                var token = _httpContextAccessor.HttpContext?.Request.Cookies["AccessToken"];
                if (string.IsNullOrEmpty(token))
                    return ""; // Không có token => Trả về mặc định

                var claims = JwtProvider.DecodeToken(token);
                var fullNameClaim = claims.FirstOrDefault(c => c.Type == "Role");

                return fullNameClaim?.Value; // Nếu không có FullName, trả về "User"
            }
            catch
            {
                return ""; // Nếu lỗi, trả về mặc định
            }

        }
    


}
}
