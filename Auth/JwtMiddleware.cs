//using Microsoft.AspNetCore.Http;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace HealthCommunitiesCheck2.Auth
//{
//    public class JwtMiddleware
//    {
//        private readonly RequestDelegate _next;
//        private readonly string _secretKey;

//        public JwtMiddleware(RequestDelegate next)
//        {
//            _next = next;
//            _secretKey = JwtSettingModel.SecretKey; // Key từ config
//        }

//        public async Task Invoke(HttpContext context)
//        {
           

//            // 🔹 Đọc token từ Cookie
//            var token = context.Request.Cookies["AccessToken"];

            

//            if (!string.IsNullOrEmpty(token))
//            {
//                AttachUserToContext(context, token);
//            }

//            await _next(context);
//        }


//        private void AttachUserToContext(HttpContext context, string token)
//        {
//            try
//            {
//                var tokenHandler = new JwtSecurityTokenHandler();
//                var key = Encoding.UTF8.GetBytes(_secretKey);

//                var claims = tokenHandler.ValidateToken(token, new TokenValidationParameters
//                {
//                    ValidateIssuerSigningKey = true,
//                    IssuerSigningKey = new SymmetricSecurityKey(key),
//                    ValidateIssuer = false,
//                    ValidateAudience = false,
//                    ClockSkew = TimeSpan.Zero
//                }, out SecurityToken validatedToken);

//                var jwtToken = (JwtSecurityToken)validatedToken;
//                var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtConstant.KeyClaim.UserId)?.Value;
//                var role = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtConstant.KeyClaim.Role)?.Value;
//                var fullName = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtConstant.KeyClaim.FullName)?.Value;

//                // Kiểm tra xem đã có dữ liệu trong Session chưa
//                if (string.IsNullOrEmpty(context.Session.GetString("FullName")))
//                {
//                    // Lưu vào HttpContext.Items (chỉ dùng trong request)
//                    context.Items["UserId"] = userId;
//                    context.Items["UserRole"] = role;

//                    // 🔹 Lưu vào Session để Razor Page có thể truy cập sau khi Redirect
//                    context.Session.SetString("FullName", fullName ?? "User");

//                    // Debug (chỉ in log khi gán giá trị mới)
//                    Console.WriteLine($"[Middleware] UserId: {userId}, FullName: {fullName}");
//                }


//            }
//            catch
//            {
//                // Token không hợp lệ => Không làm gì cả
//            }
//        }


//    }
//}
