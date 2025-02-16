using HealthCommunitiesCheck2.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HealthCommunitiesCheck2.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly IAuthService _authService;

        public LogoutModel(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<IActionResult> OnGet()
        {
            // Gọi hàm Logout từ AuthService
            await _authService.LogoutAsync();


            return RedirectToPage("/Index");
        }
    }
}
