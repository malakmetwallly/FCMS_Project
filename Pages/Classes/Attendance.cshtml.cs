using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System;

namespace FCMS_Project.Pages.Classes
{
    public class AttendanceModel : PageModel
    {
        private readonly string _connectionString;

        public AttendanceModel(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<ClassItem> Classes { get; set; } = new List<ClassItem>();
        public List<MemberItem> Members { get; set; } = new List<MemberItem>();

        [BindProperty]
        public int SelectedClassId { get; set; }

        [BindProperty]
        public List<int> SelectedMembers { get; set; }

        public void OnGet()
        {
            LoadData();
        }

        public IActionResult OnPost()
        {
            if (SelectedMembers == null || SelectedMembers.Count == 0)
            {
                LoadData();
                return Page();
            }

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                foreach (int memberId in SelectedMembers)
                {
                    

                    string query = @"INSERT INTO Attendance 
                                    (Member_ID, Receptionist_ID, Date, Time_In, Status) 
                                    VALUES 
                                    (@MemberId, 1, GETDATE(), CONVERT(TIME, GETDATE()), 'Present')";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@MemberId", memberId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            return RedirectToPage("/Index");
        }

        private void LoadData()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                
                string sqlClasses = "SELECT Class_ID, Class_Name, Date, Start_Time, End_Time FROM Class";
                using (SqlCommand cmd = new SqlCommand(sqlClasses, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Classes.Add(new ClassItem
                            {
                                Id = reader.GetInt32(0), 
                                Name = reader.GetString(1), 
                                ClassDate = reader.GetDateTime(2).ToString("yyyy-MM-dd"), 
                                Time = reader.GetTimeSpan(3).ToString()
                            });
                        }
                    }
                }

                string sqlMembers = "SELECT Member_ID, Name FROM Member";
                using (SqlCommand cmd = new SqlCommand(sqlMembers, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Members.Add(new MemberItem
                            {
                                Id = reader.GetInt32(0), 
                                Name = reader.GetString(1) 
                            });
                        }
                    }
                }
            }
        }

        public class ClassItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string ClassDate { get; set; }
            public string Time { get; set; }
        }

        public class MemberItem
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}