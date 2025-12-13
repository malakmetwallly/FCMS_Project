using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace FCMS_Project.Pages.Members
{
    public class IndexModel : PageModel
    {
        public static List<MemberVM> MembersData = new()
        {
            new MemberVM
            {
                Id = 101,
                Name = "John Doe",
                Trainer = "Captain Ahmed",
                HasPaid = true,
                IsCheckedIn = false
            },
            new MemberVM
            {
                Id = 102,
                Name = "Sara Ali",
                Trainer = "Coach Lina",
                HasPaid = false,
                IsCheckedIn = true
            }
        };

        public List<MemberVM> Members { get; set; } = new();

        public void OnGet(int? memberId)
        {
            Members = MembersData;

            if (memberId.HasValue)
            {
                Members = Members
                    .Where(m => m.Id == memberId.Value)
                    .ToList();
            }
        }

        public IActionResult OnPostToggleCheck(int id)
        {
            var member = MembersData.FirstOrDefault(m => m.Id == id);
            if (member != null)
            {
                member.IsCheckedIn = !member.IsCheckedIn;
            }

            return RedirectToPage();
        }
    }

    public class MemberVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Trainer { get; set; } = string.Empty;
        public bool HasPaid { get; set; }
        public bool IsCheckedIn { get; set; }
    }
}
