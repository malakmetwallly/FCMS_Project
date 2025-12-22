using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FCMS_Project.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; } = string.Empty;

        [BindProperty]
        public string Password { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;

        public IActionResult OnPost()
        {
            if (Username == "reception" && Password == "1234")
            {
                return RedirectToPage("/Members/Index");
            }

            Message = "Invalid login credentials";
            return Page();
        }
    }
}
