using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

public class AssignDietPlanModel : PageModel
{
	private readonly string connectionString = "Data Source=.;Initial Catalog=FCMS_DB;Integrated Security=True;TrustServerCertificate=True";

	[BindProperty] public string MemberName { get; set; }
	[BindProperty] public int TrainerId { get; set; } = 1;
	[BindProperty] public int DailyCalories { get; set; }
	[BindProperty] public int WaterGoal { get; set; }
	[BindProperty] public string Allergies { get; set; }

	// OnGet: Fetches the member's name to display it on the form
	public void OnGet(int id)
	{
		using (SqlConnection conn = new SqlConnection(connectionString))
		{
			conn.Open();
			string sql = "SELECT Name FROM Member WHERE Member_ID = @id";
			using (SqlCommand cmd = new SqlCommand(sql, conn))
			{
				cmd.Parameters.AddWithValue("@id", id);
				MemberName = cmd.ExecuteScalar()?.ToString() ?? "Member Not Found";
			}
		}
	}

	// OnPost: Saves the data to the Diet_Plan table
	public IActionResult OnPost(int id)
	{
		try
		{
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				conn.Open();
				string sql = @"INSERT INTO Diet_Plan (Member_ID, Trainer_ID, Daily_Calories, Water_Intake_Goal, Allergies, Last_Updated) 
                             VALUES (@mid, @tid, @cal, @water, @allergies, GETDATE())";

				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@mid", id);
					cmd.Parameters.AddWithValue("@tid", TrainerId);
					cmd.Parameters.AddWithValue("@cal", DailyCalories);
					cmd.Parameters.AddWithValue("@water", WaterGoal);
					cmd.Parameters.AddWithValue("@allergies", Allergies ?? "None");

					cmd.ExecuteNonQuery();
				}
			}
			return RedirectToPage("/Dashboard");
		}
		catch (Exception ex)
		{
			ModelState.AddModelError("", "Database error: " + ex.Message);
			return Page();
		}
	}
}
