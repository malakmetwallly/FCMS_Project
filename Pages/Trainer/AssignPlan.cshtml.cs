using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

public class AssignPlanModel : PageModel
{
	// Connection string - Update this with your actual local server name
	private readonly string connectionString = "Data Source=.;Initial Catalog=FCMS_DB;Integrated Security=True;TrustServerCertificate=True";

	[BindProperty]
	public int MemberId { get; set; }

	[BindProperty]
	public string MemberName { get; set; }

	[BindProperty]
	public int TrainerId { get; set; } = 1; // Defaulting to 1 based on your Insert script

	[BindProperty]
	public string FocusArea { get; set; }

	[BindProperty]
	public string WeeklySchedule { get; set; }

	public string SuccessMessage { get; set; }

	public void OnGet(int id)
	{
		MemberId = id;
		try
		{
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				conn.Open();
				string sql = "SELECT Name FROM Member WHERE Member_ID = @id";
				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@id", id);
					MemberName = cmd.ExecuteScalar()?.ToString() ?? "Unknown Member";
				}
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}

	public IActionResult OnPost(int id)
	{
		if (!ModelState.IsValid) return Page();

		try
		{
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				conn.Open();
				// We use your SQL Table structure: Member_ID, Trainer_ID, Weekly_Schedule, Focus_Area, Last_Updated
				string sql = "INSERT INTO Workout_Plan (Member_ID, Trainer_ID, Weekly_Schedule, Focus_Area, Last_Updated) " +
							 "VALUES (@mid, @tid, @schedule, @focus, GETDATE())";

				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@mid", id);
					cmd.Parameters.AddWithValue("@tid", TrainerId);
					cmd.Parameters.AddWithValue("@schedule", WeeklySchedule);
					cmd.Parameters.AddWithValue("@focus", FocusArea);

					cmd.ExecuteNonQuery();
				}
			}
		}
		catch (Exception ex)
		{
			ModelState.AddModelError("", "Error saving to database: " + ex.Message);
			return Page();
		}

		return RedirectToPage("/Dashboard"); // Redirect after success
	}
}
