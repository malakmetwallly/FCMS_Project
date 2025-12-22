using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

public class DashboardModel : PageModel
{
	private readonly string connectionString = "Data Source=.;Initial Catalog=FCMS_DB;Integrated Security=True;TrustServerCertificate=True";

	public List<MemberViewModel> MyMembers { get; set; } = new List<MemberViewModel>();

	public void OnGet()
	{
		try
		{
			using (SqlConnection conn = new SqlConnection(connectionString))
			{
				conn.Open();
				// SQL Query to get members and their current plans (if they exist)
				string sql = @"
                    SELECT 
                        m.Member_ID, 
                        m.Name, 
                        m.Email, 
                        w.Focus_Area, 
                        d.Daily_Calories 
                    FROM Member m
                    LEFT JOIN Workout_Plan w ON m.Member_ID = w.Member_ID
                    LEFT JOIN Diet_Plan d ON m.Member_ID = d.Member_ID";

				using (SqlCommand cmd = new SqlCommand(sql, conn))
				{
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							MyMembers.Add(new MemberViewModel
							{
								MemberId = reader.GetInt32(0),
								Name = reader.GetString(1),
								Email = reader.GetString(2),
								// Handling NULL values from the database
								WorkoutPlan = reader.IsDBNull(3) ? "Not Assigned" : reader.GetString(3),
								DietPlan = reader.IsDBNull(4) ? "Not Assigned" : reader.GetInt32(4).ToString() + " kcal"
							});
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine("Database Error: " + ex.Message);
		}
	}
}

public class MemberViewModel
{
	public int MemberId { get; set; }
	public string Name { get; set; }
	public string Email { get; set; }
	public string WorkoutPlan { get; set; }
	public string DietPlan { get; set; }
}
