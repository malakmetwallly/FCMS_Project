using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FCMS_Project.Pages
{
    public class LoginModel : PageModel
    {
        public void OnGet()
        {
        }
        
        public IActionResult OnPost()
        {
            return RedirectToPage("/Admin/Dashboard");
        }
    }
}