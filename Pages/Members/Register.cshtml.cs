using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MemberWebApp.Data;
using MemberWebApp.Models;

namespace MemberWebApp.Pages.Members
{
    public class RegisterModel : PageModel
    {
        private readonly FcmsContext _context;

        public RegisterModel(FcmsContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Member Member { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Member.Join_Date = DateTime.Today;
            Member.Last_Updated = DateTime.Today;

            _context.Members.Add(Member);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}