using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace FCMS_Project.Pages.Members
{
    public class RegisterModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public RegisterModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty] public string Name { get; set; } = "";
        [BindProperty] public string Email { get; set; } = "";
        [BindProperty] public string Phone { get; set; } = "";
        [BindProperty] public int Age { get; set; }
        [BindProperty] public decimal Height { get; set; }
        [BindProperty] public decimal Weight { get; set; }
        [BindProperty] public string Gender { get; set; } = "";
        [BindProperty] public string Membership_Type { get; set; } = "";
        [BindProperty] public string Goal { get; set; } = "";
        [BindProperty] public string TrainerName { get; set; } = string.Empty;

        public string? SuccessMessage { get; set; }
        public string? ErrorMessage { get; set; }

        public IActionResult OnPost()
        {
            string cs = _configuration.GetConnectionString("DefaultConnection")!;

            try
            {
                using SqlConnection conn = new SqlConnection(cs);
                conn.Open();

                string sql = @"
                INSERT INTO Member
                (Name, Email, Phone, Age, Height, Weight, Gender, Join_Date, Membership_Type, Goal, Last_Updated)
                VALUES
                (@Name, @Email, @Phone, @Age, @Height, @Weight, @Gender, GETDATE(), @Membership, @Goal, GETDATE())";

                using SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Phone", Phone);
                cmd.Parameters.AddWithValue("@Age", Age);
                cmd.Parameters.AddWithValue("@Height", Height);
                cmd.Parameters.AddWithValue("@Weight", Weight);
                cmd.Parameters.AddWithValue("@Gender", Gender);
                cmd.Parameters.AddWithValue("@Membership", Membership_Type);
                cmd.Parameters.AddWithValue("@Goal", Goal);

                cmd.ExecuteNonQuery();
                SuccessMessage = "Member registered successfully!"; 
            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                ErrorMessage = "Email already exists.";
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return Page();
        }
    }
}
