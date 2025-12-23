using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace FCMS_Project.Pages.Admin
{
    public class AssignTrainerModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public AssignTrainerModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<SelectListItem> MemberOptions { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> TrainerOptions { get; set; } = new List<SelectListItem>();

        [BindProperty] public int SelectedMemberId { get; set; }
        [BindProperty] public int SelectedTrainerId { get; set; }
        [BindProperty] public string Goal { get; set; }

        public void OnGet()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                using (SqlCommand cmd = new SqlCommand("SELECT Member_ID, Name FROM Member", con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MemberOptions.Add(new SelectListItem
                            {
                                Value = reader["Member_ID"].ToString(),
                                Text = reader["Name"].ToString()
                            });
                        }
                    }
                }

                using (SqlCommand cmd = new SqlCommand("SELECT Trainer_ID, Name FROM Trainer", con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TrainerOptions.Add(new SelectListItem
                            {
                                Value = reader["Trainer_ID"].ToString(),
                                Text = reader["Name"].ToString()
                            });
                        }
                    }
                }
            }
        }

        public IActionResult OnPost()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query1 = "INSERT INTO Workout_Plan (Member_ID, Trainer_ID, Weekly_Schedule, Focus_Area, Last_Updated) VALUES (@Mid, @Tid, 'Pending', @Goal, GETDATE())";
                SqlCommand cmd1 = new SqlCommand(query1, con);
                cmd1.Parameters.AddWithValue("@Mid", SelectedMemberId);
                cmd1.Parameters.AddWithValue("@Tid", SelectedTrainerId);
                cmd1.Parameters.AddWithValue("@Goal", Goal);
                cmd1.ExecuteNonQuery();

                string query2 = "INSERT INTO Diet_Plan (Member_ID, Trainer_ID, Daily_Calories, Water_Intake_Goal, Allergies, Last_Updated) VALUES (@Mid, @Tid, 0, 0, 'Pending', GETDATE())";
                SqlCommand cmd2 = new SqlCommand(query2, con);
                cmd2.Parameters.AddWithValue("@Mid", SelectedMemberId);
                cmd2.Parameters.AddWithValue("@Tid", SelectedTrainerId);
                cmd2.ExecuteNonQuery();

                string query3 = "UPDATE Member SET Goal = @Goal WHERE Member_ID = @Mid";
                SqlCommand cmd3 = new SqlCommand(query3, con);
                cmd3.Parameters.AddWithValue("@Goal", Goal);
                cmd3.Parameters.AddWithValue("@Mid", SelectedMemberId);
                cmd3.ExecuteNonQuery();
            }

            return RedirectToPage("/Admin/Dashboard");
        }
    }
}