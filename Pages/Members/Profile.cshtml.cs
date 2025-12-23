using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using FCMS_Project.Models;

namespace FCMS_Project.Pages.Members
{
    public class ProfileModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public ProfileModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Member? Member { get; set; }
        public Trainer? Trainer { get; set; } // Add this property

        public IActionResult OnGet(int id)
        {
            string cs = _configuration.GetConnectionString("DefaultConnection")!;
            using SqlConnection conn = new SqlConnection(cs);
            conn.Open();

            string sql = "SELECT * FROM Member WHERE Member_ID = @id";
            using SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@id", id);

            using SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.Read())
                return NotFound();

            Member = new Member
            {
                Member_ID = (int)reader["Member_ID"],
                Name = reader["Name"].ToString()!,
                Email = reader["Email"].ToString()!,
                Phone = reader["Phone"].ToString()!,
                Age = (int)reader["Age"],
                Height = (double)(decimal)reader["Height"],
                Weight = (double)(decimal)reader["Weight"],
                Gender = reader["Gender"].ToString()!,
                Join_Date = (DateTime)reader["Join_Date"],
                Membership_Type = reader["Membership_Type"].ToString()!,
                Goal = reader["Goal"].ToString()!,
                Last_Updated = (DateTime)reader["Last_Updated"]
            };

            // Example: Load Trainer from your data source
            // Trainer = /* fetch assigned trainer for member, or null if not assigned */;

            return Page();
        }
    }

    // Ensure these classes exist or are imported from your models
    public class Member
    {
        public int Member_ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public DateTime Join_Date { get; set; }
        public string Membership_Type { get; set; }
        public string Goal { get; set; }
        public DateTime Last_Updated { get; set; }
    }

    public class Trainer
    {
        public string Name { get; set; }
        // Add other properties as needed
    }
}
