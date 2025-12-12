using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace FCMS_Phase1.Pages.Classes
{
    public class ScheduleModel : PageModel
    {
        public List<ClassItem> WeeklyClasses { get; set; }

        public void OnGet()
        {
            
            WeeklyClasses = new List<ClassItem>
            {
                new ClassItem { Day="Mon", Time="9 AM", Name="Yoga", Trainer="Sara" },
                new ClassItem { Day="Mon", Time="11 AM", Name="Pilates", Trainer="Omar" },
                new ClassItem { Day="Tue", Time="10 AM", Name="HIIT", Trainer="Ali" },
                new ClassItem { Day="Wed", Time="5 PM", Name="Boxing", Trainer="Mona" },
                new ClassItem { Day="Thu", Time="6 PM", Name="Zumba", Trainer="Sara" },
                new ClassItem { Day="Fri", Time="7 AM", Name="Spinning", Trainer="Omar" }
            };
        }

        public class ClassItem
        {
            public string Day { get; set; }
            public string Time { get; set; }
            public string Name { get; set; }
            public string Trainer { get; set; }
        }
    }
}
