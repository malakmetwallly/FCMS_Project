using FCMS_Project.Pages.Members;
using Microsoft.Ajax.Utilities;

namespace FCMS_Project.Models
{
    public class Member
    {
        public int Member_ID { get; set; }

        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";

        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        public int Age { get; set; }

        public string Gender { get; set; } = "";
        public DateTime Join_Date { get; set; }

        public string Membership_Type { get; set; } = "";
        public string Goal { get; set; } = "";

        public DateTime Last_Updated { get; set; }
    }
}
