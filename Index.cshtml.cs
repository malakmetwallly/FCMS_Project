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
            },
            new MemberVM
            {
                Id = 103,
                Name = "Ahmed Hassan",
                Trainer = "Captain Omar",
                HasPaid = true,
                IsCheckedIn = true
            },
            new MemberVM
            {
                Id = 104,
                Name = "Mona Adel",
                Trainer = "Coach Lina",
                HasPaid = true,
                IsCheckedIn = false
            },
            new MemberVM
            {
                Id = 105,
                Name = "Khaled Samir",
                Trainer = "Captain Ahmed",
                HasPaid = false,
                IsCheckedIn = false
            },
            new MemberVM
            {
                Id = 106,
                Name = "Nour ElDin",
                Trainer = "Captain Omar",
                HasPaid = true,
                IsCheckedIn = true
            },
            new MemberVM
            {
                Id = 107,
                Name = "Huda Mahmoud",
                Trainer = "Coach Lina",
                HasPaid = true,
                IsCheckedIn = false
            },
            new MemberVM
            {
                Id = 108,
                Name = "Omar Youssef",
                Trainer = "Captain Ahmed",
                HasPaid = false,
                IsCheckedIn = false
            },
            new MemberVM
            {
                Id = 109,
                Name = "Salma Fathy",
                Trainer = "Coach Lina",
                HasPaid = true,
                IsCheckedIn = true
            },
            new MemberVM
            {
                Id = 110,
                Name = "Youssef Adel",
                Trainer = "Captain Omar",
                HasPaid = true,
                IsCheckedIn = false
            },
            new MemberVM
            {
                Id = 111,
                Name = "Mai Hassan",
                Trainer = "Coach Lina",
                HasPaid = false,
                IsCheckedIn = true
            }
        };

        public List<MemberVM> Members { get; set; } = new();

        public void OnGet(int? memberId)
        {
            if (memberId.HasValue && memberId > 0)
            {
                Members = MembersData
                    .Where(m => m.Id == memberId.Value)
                    .ToList();
            }
            else
            {
                Members = MembersData;
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

