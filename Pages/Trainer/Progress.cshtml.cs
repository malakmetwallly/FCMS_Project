using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

public class ProgressModel : PageModel
{
	private readonly string connectionString = "Data Source=.;Initial Catalog=FCMS_DB;Integrated Security=True;TrustServerCertificate=True";

	[BindProperty] public string MemberName { get; set; }
	[BindProperty] public int TrainerId { get; set; } = 1; // Assuming Trainer with ID 1 is logged in
	[BindProperty] public decimal Weight { get; set; }
	[BindProperty] public decimal MuscleMass { get; set; }
	[BindProperty] public decimal BodyFat { get; set; }
	[BindProperty] public string Notes { get; set; }

	public void OnGet(int id)
	{
		// Get member name to show who we are logging for
		using (SqlConnection conn = new SqlConnection(connectionString))
		{
			conn.Open();
			string sql = "SELECT Name FROM Member WHERE Member_ID = @id";
			using (SqlCommand cmd = new SqlCommand(sql, conn))
			{
				cmd.Parameters.AddWithValue("@id", id);
				MemberName = cmd.ExecuteScalar()?.ToString() ?? "Member";
			}
		}
	}

	public IActionResult OnPost(int id)
	{
		try
		{
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				conn.Open();
				// SQL based on your Progress table: Progress_ID is IDENTITY, Date_Recorded defaults to GETDATE()
				string sql = @"INSERT INTO Progress (Member_ID, Trainer_ID, Weight, Muscle_Mass, Body_Fat_Percentage, Notes, Date_Recorded) 
                             VALUES (@mid, @tid, @w, @m, @f, @n, GETDATE())";

				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					cmd.Parameters.AddWithValue("@mid", id);
					cmd.Parameters.AddWithValue("@tid", TrainerId);
					cmd.Parameters.AddWithValue("@w", Weight);
					cmd.Parameters.AddWithValue("@m", MuscleMass);
					cmd.Parameters.AddWithValue("@f", BodyFat);
					cmd.Parameters.AddWithValue("@n", (object)Notes ?? DBNull.Value);

					cmd.ExecuteNonQuery();
				}
			}
			return RedirectToPage("/Dashboard");
		}
		catch (Exception ex)
		{
			ModelState.AddModelError("", "Error saving progress: " + ex.Message);
			return Page();
		}
	}
}
