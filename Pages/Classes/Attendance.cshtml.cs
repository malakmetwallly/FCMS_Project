using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace FCMS_Phase1.Pages.Classes
{
    public class AttendanceModel : PageModel
    {
        public List<ClassItem> Classes { get; set; }
        public List<MemberItem> Members { get; set; }

        public List<string> PresentMembers { get; set; }

        public void OnGet()
        {
            Classes = new List<ClassItem>
            {
                new ClassItem { Id = 1, Name="Yoga", Trainer="Sara", Day="Mon", Time="9 AM" },
                new ClassItem { Id = 2, Name="HIIT", Trainer="Ali", Day="Tue", Time="10 AM" },
                new ClassItem { Id = 3, Name="Boxing", Trainer="Mona", Day="Wed", Time="5 PM" }
            };

            Members = new List<MemberItem>
            {
                new MemberItem { Id=1, Name="Ahmed" },
                new MemberItem { Id=2, Name="Mona" },
                new MemberItem { Id=3, Name="Omar" },
                new MemberItem { Id=4, Name="Sara" }
            };

            PresentMembers = new List<string>(); 
        }

        
        public class ClassItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Trainer { get; set; }
            public string Day { get; set; }
            public string Time { get; set; }
        }

        public class MemberItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
