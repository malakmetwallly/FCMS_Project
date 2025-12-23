using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace FCMS_Project.Pages
{
    public class LoginModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public LoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string queryAdmin = "SELECT Count(*) FROM Admin WHERE Email=@E AND Password=@P";
                using (SqlCommand cmd = new SqlCommand(queryAdmin, con))
                {
                    cmd.Parameters.AddWithValue("@E", Email);
                    cmd.Parameters.AddWithValue("@P", Password);
                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0) return RedirectToPage("/Admin/Dashboard");
                }
                
                string queryTrainer = "SELECT Count(*) FROM Trainer WHERE Email=@E AND Phone=@P";
                using (SqlCommand cmd = new SqlCommand(queryTrainer, con))
                {
                    cmd.Parameters.AddWithValue("@E", Email);
                    cmd.Parameters.AddWithValue("@P", Password);
                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0) return RedirectToPage("/Trainer/Dashboard");
                }

                string queryRec = "SELECT Count(*) FROM Receptionist WHERE Email=@E AND Password=@P";
                using (SqlCommand cmd = new SqlCommand(queryRec, con))
                {
                    cmd.Parameters.AddWithValue("@E", Email);
                    cmd.Parameters.AddWithValue("@P", Password);
                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0) return RedirectToPage("/Members/Index");
                }

                string queryMem = "SELECT Count(*) FROM Member WHERE Email=@E AND Phone=@P";
                using (SqlCommand cmd = new SqlCommand(queryMem, con))
                {
                    cmd.Parameters.AddWithValue("@E", Email);
                    cmd.Parameters.AddWithValue("@P", Password);
                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0) return RedirectToPage("/Members/Profile");
                }
            }

            ErrorMessage = "Invalid Email or Password";
            return Page();
        }
    }
}