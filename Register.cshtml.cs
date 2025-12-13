using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FCMS_Project.Pages.Members
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; } = string.Empty;

        [BindProperty]
        public int Age { get; set; }

        [BindProperty]
        public int Height { get; set; }

        [BindProperty]
        public decimal Weight { get; set; }

        [BindProperty]
        public string Goal { get; set; } = string.Empty;

        [BindProperty]
        public string Trainer { get; set; } = string.Empty;

        [BindProperty]
        public bool HasPaid { get; set; }

        public IActionResult OnPost()
        {
            // TODO: Insert into DB later
            return RedirectToPage("/Members/Index");
        }
    }
}
