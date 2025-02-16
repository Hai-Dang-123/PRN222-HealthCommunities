using HealthCommunitiesCheck2.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HealthCommunitiesCheck2.Pages.Shared
{
    public class _HeaderModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public string FullName { get; set; }
        //public string Avatar { get; set; }
        private readonly UserUtility _userUtility;

        public _HeaderModel(IHttpContextAccessor httpContextAccessor, UserUtility userUtility)
        {
            _httpContextAccessor = httpContextAccessor;
            _userUtility = userUtility;
        }

        public void OnGet()
        {

            ////FullName = _httpContextAccessor.HttpContext?.Session.GetString("FullName") ?? "User";
            ////Console.WriteLine("tên mày nè :" + FullName);
            //var fullNameClaim = User.FindFirst("FullName");
            //FullName = fullNameClaim?.Value ?? "Không có tên";

            //// 🛠 Debug để kiểm tra xem Claims có tồn tại trong Razor Page không
            //Console.WriteLine($"[Razor] Lấy từ Claims: {FullName}");

            //FullName = _userUtility.GetFullNameFromToken();
            //Console.WriteLine($"[Razor Page] FullName: {FullName}");
        }
    }
}
