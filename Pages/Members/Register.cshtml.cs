using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace FCMS_Project.Pages.Members
{
    public class RegisterModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public RegisterModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // ====== FORM FIELDS ======
        [BindProperty] public string Name { get; set; }
        [BindProperty] public string Email { get; set; }
        [BindProperty] public string Phone { get; set; }
        [BindProperty] public int Age { get; set; }
        [BindProperty] public decimal Height { get; set; }
        [BindProperty] public decimal Weight { get; set; }
        [BindProperty] public string Gender { get; set; }
        [BindProperty] public string Membership_Type { get; set; }
        [BindProperty] public string Goal { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Email))
            {
                ErrorMessage = "Name and Email are required.";
                return Page();
            }

            string connectionString = _configuration.GetConnectionString("DefaultConnection");

            try
            {
                using SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();

                // ===== CHECK DUPLICATE EMAIL =====
                string checkQuery = "SELECT COUNT(*) FROM Member WHERE Email = @Email";
                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@Email", Email);
                    int exists = (int)checkCmd.ExecuteScalar();

                    if (exists > 0)
                    {
                        ErrorMessage = "This email is already registered.";
                        return Page();
                    }
                }

                // ===== INSERT MEMBER =====
                string insertQuery = @"
                    INSERT INTO Member
                    (Name, Email, Phone, Weight, Height, Age, Gender, Join_Date, Membership_Type, Goal, Last_Updated)
                    VALUES
                    (@Name, @Email, @Phone, @Weight, @Height, @Age, @Gender, GETDATE(), @Membership_Type, @Goal, GETDATE())";

                using SqlCommand cmd = new SqlCommand(insertQuery, conn);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Phone", Phone ?? "");
                cmd.Parameters.AddWithValue("@Weight", Weight);
                cmd.Parameters.AddWithValue("@Height", Height);
                cmd.Parameters.AddWithValue("@Age", Age);
                cmd.Parameters.AddWithValue("@Gender", Gender ?? "");
                cmd.Parameters.AddWithValue("@Membership_Type", Membership_Type ?? "");
                cmd.Parameters.AddWithValue("@Goal", Goal ?? "");

                cmd.ExecuteNonQuery();

                SuccessMessage = "Member registered successfully!";
                ModelState.Clear();
            }
            catch (SqlException ex)
            {
                ErrorMessage = ex.Message;
            }

            return Page();
        }
    }
}
