using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FCMS_Project.Pages.Members
{
    public class ProfileModel : PageModel
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public int Height { get; set; }
        public decimal Weight { get; set; }
        public string Goal { get; set; } = string.Empty;
        public string Trainer { get; set; } = string.Empty;
        public bool HasPaid { get; set; }
        public bool IsCheckedIn { get; set; }

        public void OnGet(int id)
        {
            // Dummy data
            Name = "John Doe";
            Age = 25;
            Height = 175;
            Weight = 78;
            Goal = "Weight Loss";
            Trainer = "Captain Ahmed";
            HasPaid = true;
            IsCheckedIn = false;
        }
    }
}
