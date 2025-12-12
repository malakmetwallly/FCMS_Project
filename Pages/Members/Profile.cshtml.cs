using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MemberWebApp.Data;
using MemberWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MemberWebApp.Pages.Members
{
    public class ProfileModel : PageModel
    {
        private readonly FcmsContext _context;

        public ProfileModel(FcmsContext context)
        {
            _context = context;
        }

        public Member Member { get; set; }
        public Trainer Trainer { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Member = await _context.Members.FindAsync(id);

            if (Member == null)
            {
                return NotFound();
            }

            var workoutPlan = await _context.Workout_Plans.FirstOrDefaultAsync(p => p.Member_ID == id);
            if (workoutPlan != null)
            {
                Trainer = await _context.Trainers.FindAsync(workoutPlan.Trainer_ID);
            }

            return Page();
        }
    }
}