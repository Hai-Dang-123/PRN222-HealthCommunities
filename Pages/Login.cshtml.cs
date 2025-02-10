using HealthCommunitiesCheck2.DTO;
using HealthCommunitiesCheck2.IService;
using HealthCommunitiesCheck2.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace HealthCommunitiesCheck2.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IAuthService _authService;

        public LoginModel(IAuthService authService)
        {
            _authService = authService;
        }

        [BindProperty]
        public LoginDTO LoginData { get; set; } = new LoginDTO();

        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var response = await _authService.LoginAsync(LoginData);

            if (!response.IsSuccess)
            {
                ErrorMessage = response.Message;
                return Page();
            }

            var token = response.Result as dynamic;
            var accessToken = token.AccessToken;
            var refreshToken = token.RefeshToken;
            

            if (string.IsNullOrEmpty(accessToken))
            {
                ModelState.AddModelError(string.Empty, "Không lấy được Access Token");
                return Page();
            }

            // 🔹 Lưu token vào Cookie
            Response.Cookies.Append("AccessToken", accessToken, new CookieOptions
            {
                HttpOnly = true, // Bảo mật, không thể truy cập từ JavaScript
                Secure = true,   // Chỉ gửi qua HTTPS
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(1) 
            });

            // 🔹 Lưu RefreshToken vào Cookie
            Response.Cookies.Append("RefreshToken", refreshToken, new CookieOptions
            {
                HttpOnly = true, // Ngăn chặn truy cập từ JavaScript
                Secure = true,   // Chỉ gửi qua HTTPS
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(30) // Hết hạn
            });


            return RedirectToPage("/Index");
        }
    }
}
