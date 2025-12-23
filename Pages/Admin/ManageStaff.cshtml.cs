using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace FCMS_Project.Pages.Admin
{
    public class ManageStaffModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public ManageStaffModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty] public string FullName { get; set; }
        [BindProperty] public string Email { get; set; }
        [BindProperty] public string Phone { get; set; }
        [BindProperty] public int Salary { get; set; }
        [BindProperty] public string Role { get; set; }
        [BindProperty] public string Specialty { get; set; }
        [BindProperty] public int Experience { get; set; }
        [BindProperty] public string Username { get; set; }
        [BindProperty] public string Password { get; set; }
        [BindProperty] public string ShiftType { get; set; }

        public class StaffInfo
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Role { get; set; }
        }
        public List<StaffInfo> StaffList = new List<StaffInfo>();

        public void OnGet()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT Name, Email, 'Trainer' AS Role FROM Trainer UNION SELECT Name, Email, 'Receptionist' AS Role FROM Receptionist";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            StaffInfo s = new StaffInfo();
                            s.Name = reader.GetString(0);
                            s.Email = reader.GetString(1);
                            s.Role = reader.GetString(2);
                            StaffList.Add(s);
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
                if (Role == "Trainer")
                {
                    string query = "INSERT INTO Trainer (Name, Email, Phone, Specialty, Experience_Years, Salary, Admin_ID) VALUES (@Name, @Email, @Phone, @Spec, @Exp, @Sal, 1)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Name", FullName);
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@Phone", Phone);
                    cmd.Parameters.AddWithValue("@Spec", Specialty);
                    cmd.Parameters.AddWithValue("@Exp", Experience);
                    cmd.Parameters.AddWithValue("@Sal", Salary);
                    cmd.ExecuteNonQuery();
                }
                else if (Role == "Receptionist")
                {
                    string query = "INSERT INTO Receptionist (Name, Email, Phone, Username, Password, Shift_Type, Salary, Admin_ID) VALUES (@Name, @Email, @Phone, @User, @Pass, @Shift, @Sal, 1)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Name", FullName);
                    cmd.Parameters.AddWithValue("@Email", Email);
                    cmd.Parameters.AddWithValue("@Phone", Phone);
                    cmd.Parameters.AddWithValue("@User", Username);
                    cmd.Parameters.AddWithValue("@Pass", Password);
                    cmd.Parameters.AddWithValue("@Shift", ShiftType);
                    cmd.Parameters.AddWithValue("@Sal", Salary);
                    cmd.ExecuteNonQuery();
                }
            }
            return RedirectToPage();
        }

        public IActionResult OnPostDelete(string emailToDelete, string roleToDelete)
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                if (roleToDelete == "Trainer")
                {
                    int trainerId = 0;
                    string getIdQuery = "SELECT Trainer_ID FROM Trainer WHERE Email = @Email";
                    using (SqlCommand cmd = new SqlCommand(getIdQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@Email", emailToDelete);
                        object result = cmd.ExecuteScalar();
                        if (result != null) trainerId = (int)result;
                    }

                    if (trainerId > 0)
                    {
                        string cleanupQuery = @"
                            DELETE FROM Class_Enrollment WHERE Trainer_ID = @TID;
                            DELETE FROM Workout_Plan WHERE Trainer_ID = @TID;
                            DELETE FROM Diet_Plan WHERE Trainer_ID = @TID;
                            DELETE FROM Progress WHERE Trainer_ID = @TID;
                            DELETE FROM Class WHERE Trainer_ID = @TID;
                            DELETE FROM Trainer WHERE Trainer_ID = @TID;";

                        using (SqlCommand cmd = new SqlCommand(cleanupQuery, con))
                        {
                            cmd.Parameters.AddWithValue("@TID", trainerId);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                else if (roleToDelete == "Receptionist")
                {
                    string query = "DELETE FROM Receptionist WHERE Email = @Email";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Email", emailToDelete);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            return RedirectToPage();
        }
    }
}