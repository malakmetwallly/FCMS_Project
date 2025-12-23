using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace FCMS_Project.Pages.Admin
{
    public class DashboardModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public DashboardModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public int TotalTrainers { get; set; }
        public int TotalMembers { get; set; }
        public int EstRevenue { get; set; }

        public void OnGet()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) FROM Trainer", con);
                TotalTrainers = (int)cmd1.ExecuteScalar();

                SqlCommand cmd2 = new SqlCommand("SELECT COUNT(*) FROM Member", con);
                TotalMembers = (int)cmd2.ExecuteScalar();

                SqlCommand cmd3 = new SqlCommand("SELECT COUNT(*) * 1000 FROM Member", con);
                EstRevenue = (int)cmd3.ExecuteScalar();
            }
        }
    }
}