using System.Security.Claims;
using HealthCommunitiesCheck2.Auth;
using HealthCommunitiesCheck2.DTO;
using HealthCommunitiesCheck2.IService;
using HealthCommunitiesCheck2.Model;
using HealthCommunitiesCheck2.UnitOfWork;

namespace HealthCommunitiesCheck2.Services
{
    public class AuthService : IAuthService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService (IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseDTO> LoginAsync(LoginDTO loginData)
        {
            // tìm user bằng email
            var user = await _unitOfWork.User.FindByEmailAsync(loginData.Email);
            if (user == null)
            {
                return new ResponseDTO("User not found", 404, false);
            }
            // kiểm tra mật khẩu

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginData.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                return new ResponseDTO("Invalid email or password.", 400, false);
            }
            //kiểm tra refreshToken
            var exitsRefreshToken = await _unitOfWork.Token.GetRefreshTokenByUserID(user.UserID);
            if (exitsRefreshToken != null)
            {
                // nếu có thì thu hồi
                exitsRefreshToken.IsRevoked = true;
                await _unitOfWork.Token.UpdateAsync(exitsRefreshToken); // cập nhật
            }
            //khởi tạo claim
            var claims = new List<Claim>();

            //thêm role 
            var userRole = await _unitOfWork.Role.GetByIdAsync(user.RoleID);
            var userRoleName = userRole?.RoleName ?? "User";

            claims.Add(new Claim(JwtConstant.KeyClaim.Role, userRoleName));


            //thêm email
            claims.Add(new Claim(JwtConstant.KeyClaim.Email, user.Email));

            //thêm id
            claims.Add(new Claim(JwtConstant.KeyClaim.UserId, user.UserID.ToString()));

            //thêm name
            claims.Add(new Claim(JwtConstant.KeyClaim.FullName, user.FullName));

            //tạo refesh token
            var refreshTokenKey = JwtProvider.GenerateRefreshToken(claims);

            //tạo access token
            var accessTokenKey = JwtProvider.GenerateAccessToken(claims);

            //new refreshToken model
            var refreshToken = new RefreshToken
            {
                RefreshTokenId = Guid.NewGuid(),
                UserId = user.UserID,
                RefreshTokenKey = refreshTokenKey,
                IsRevoked = false,
                CreatedAt = DateTime.UtcNow
            };
            _unitOfWork.Token.Add(refreshToken);
            try
            {
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Error saving refresh token: {ex.Message}", 500, false);
            }
            //var token = GenerateJwtToken(user);
            return new ResponseDTO("Login successful", 200, true, new
                {
                AccessToken = accessTokenKey,
                RefeshToken = refreshTokenKey,
                
            });


        }
        public async Task<ResponseDTO> RefreshBothTokens(string oldAccessToken, string refreshTokenKey)
        {
            // Kiểm tra tính hợp lệ của refresh token
            var claimsPrincipal = JwtProvider.Validation(refreshTokenKey);
            if (claimsPrincipal == null)
            {
                return new ResponseDTO("Invalid refresh token", 400, false);
            }

            // Lấy đối tượng RefreshToken từ refresh token Key
            var refreshTokenDTO = await _unitOfWork.Token.GetRefreshTokenByKey(refreshTokenKey);
            if (refreshTokenDTO == null || refreshTokenDTO.IsRevoked)
            {
                return new ResponseDTO("Refresh token not found or has been revoked", 403, false);
            }

            // Kiểm tra nếu refresh token đã hết hạn
            var tokenExpirationDate = refreshTokenDTO.CreatedAt.AddDays(JwtSettingModel.ExpireDayRefreshToken);
            if (DateTime.UtcNow > tokenExpirationDate)
            {
                return new ResponseDTO("Refresh token expired, please login again", 403, false);
            }

            // Lấy thông tin người dùng từ UserId
            var user = await _unitOfWork.User.GetByIdAsync(refreshTokenDTO.UserId);
            if (user == null)
            {
                return new ResponseDTO("User not found", 404, false);
            }

            // Khởi tạo danh sách claims
            var claims = new List<Claim>();

            // Thêm email vào claims
            claims.Add(new Claim(JwtConstant.KeyClaim.Email, user.Email));

            // Thêm vai trò vào claims
            var userRole = await _unitOfWork.Role.GetByIdAsync(user.RoleID);
            var userRoleName = userRole?.RoleName ?? "User";

            claims.Add(new Claim(JwtConstant.KeyClaim.Role, userRoleName));

            // Thêm UserId vào claims
            claims.Add(new Claim(JwtConstant.KeyClaim.UserId, user.UserID.ToString()));


            // Tạo access token mới
            var newAccessToken = JwtProvider.GenerateAccessToken(claims);

            // Lưu refresh token mới vào database
            var newRefreshToken = new RefreshToken
            {
                RefreshTokenId = Guid.NewGuid(),
                UserId = user.UserID,
                RefreshTokenKey = refreshTokenKey,
                IsRevoked = false,
                CreatedAt = DateTime.UtcNow // Lưu thời gian tạo
            };

            // Xóa refresh token cũ
             _unitOfWork.Token.Delete(refreshTokenDTO);
            // Thêm refresh token mới
             _unitOfWork.Token.Add(newRefreshToken);
            try
            {
                await _unitOfWork.SaveChangeAsync();
            }
            catch (Exception ex)
            {
                return new ResponseDTO($"Error refreshing tokens: {ex.Message}", 500, false);
            }

            return new ResponseDTO("Token refreshed successfully", 200, true);
        }

        public async Task<ResponseDTO> LogoutAsync()
        {
            var context = _httpContextAccessor.HttpContext;

            if (context != null)
            {
                // Lấy refreshToken từ Cookie
                var refreshToken = context.Request.Cookies["RefreshToken"];

                // Kiểm tra nếu có refreshToken thì thu hồi
                if (!string.IsNullOrEmpty(refreshToken))
                {
                    var token = await _unitOfWork.Token.GetRefreshTokenByKey(refreshToken);
                    if (token != null)
                    {
                        token.IsRevoked = true;
                        _unitOfWork.Token.UpdateAsync(token);
                        await _unitOfWork.SaveChangeAsync();
                        Console.WriteLine($"[AuthService] RefreshToken {refreshToken} đã bị thu hồi!");
                    }
                }

                //// Xóa session
                //context.Session.Clear();

                // Xóa cookies (AccessToken & RefreshToken)
                context.Response.Cookies.Delete("AccessToken");
                context.Response.Cookies.Delete("RefreshToken");

                Console.WriteLine("[AuthService] Đăng xuất thành công!");
            }


            return new ResponseDTO("Logout successful", 200, true);
            
        }
    }
}
