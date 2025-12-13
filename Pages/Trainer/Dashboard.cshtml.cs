using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

public class DashboardModel : PageModel
{
	public List<MemberViewModel> MyMembers { get; set; }

	public void OnGet()
	{
		// Dummy data
		MyMembers = new List<MemberViewModel>
		{
			new MemberViewModel
			{
				Name = "Ahmed Ali",
				Email = "ahmed@email.com",
				WorkoutPlan = "Beginner",
				DietPlan = "Low Carb"
			},
			new MemberViewModel
			{
				Name = "Sara Mohamed",
				Email = "sara@email.com",
				WorkoutPlan = "Strength",
				DietPlan = "Keto"
			}
		};
	}
}

public class MemberViewModel
{
	public string Name { get; set; }
	public string Email { get; set; }
	public string WorkoutPlan { get; set; }
	public string DietPlan { get; set; }
}
