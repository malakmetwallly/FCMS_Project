using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace FCMS_Phase1.Pages.Classes
{
    public class BookClassModel : PageModel
    {
        public List<ClassItem> Classes { get; set; }

        [BindProperty]
        public int SelectedClassId { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
            Classes = new List<ClassItem>
            {
                new ClassItem { Id = 1, Name="Yoga", Trainer="Sara", Day="Mon", Time="9 AM" },
                new ClassItem { Id = 2, Name="HIIT", Trainer="Ali", Day="Tue", Time="10 AM" },
                new ClassItem { Id = 3, Name="Boxing", Trainer="Mona", Day="Wed", Time="5 PM" }
            };
        }

        public void OnPost()
        {
            
            Classes = new List<ClassItem>
            {
                new ClassItem { Id = 1, Name="Yoga", Trainer="Sara", Day="Mon", Time="9 AM" },
                new ClassItem { Id = 2, Name="HIIT", Trainer="Ali", Day="Tue", Time="10 AM" },
                new ClassItem { Id = 3, Name="Boxing", Trainer="Mona", Day="Wed", Time="5 PM" }
            };

            var selectedClass = Classes.Find(c => c.Id == SelectedClassId);

            if (selectedClass != null)
            {
                Message = $"You successfully joined {selectedClass.Name} ({selectedClass.Day} - {selectedClass.Time})";
            }
            else
            {
                Message = "Please select a class.";
            }
        }

        public class ClassItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Trainer { get; set; }
            public string Day { get; set; }
            public string Time { get; set; }
        }
    }
}
