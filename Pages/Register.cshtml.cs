using HealthCommunitiesCheck2.DTO;
using HealthCommunitiesCheck2.IService;
using HealthCommunitiesCheck2.Services;
using HealthCommunitiesCheck2.UnitOfWork;
using HealthCommunitiesCheck2.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealthCommunitiesCheck2.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly IRegisterService _registerService;

        public RegisterModel(IRegisterService registerService)
        {
            _registerService = registerService;
        }
        public string ErrorMessage { get; set; }

        [BindProperty]
        public RegisterDTO RegisterData { get; set; } = new RegisterDTO();

        public void OnGet() { } // Không cần lấy Role từ DB

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var response = await _registerService.Register(RegisterData);

            if (!response.IsSuccess)
            {
                ErrorMessage = response.Message;
                return Page();
            }

            return RedirectToPage("/Login");
        }
    }

}
