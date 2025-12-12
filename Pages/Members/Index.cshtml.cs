using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MemberWebApp.Data;
using MemberWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MemberWebApp.Pages.Members
{
    public class IndexModel : PageModel
    {
        private readonly FcmsContext _context;

        public IndexModel(FcmsContext context)
        {
            _context = context;
        }

        public IList<Member> Members { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Search { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.Members.AsQueryable();

            if (!string.IsNullOrEmpty(Search))
            {
                query = query.Where(m => EF.Functions.Like(m.Name, $"%{Search}%"));
            }

            Members = await query.ToListAsync();
        }
    }
}