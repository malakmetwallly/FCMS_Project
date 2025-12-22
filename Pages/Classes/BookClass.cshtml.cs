using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;

namespace FCMS_Project_New.Pages.Classes 
{
    public class BookClassModel : PageModel
    {
        private readonly string _connectionString;

        public BookClassModel(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<ClassItem> Classes { get; set; } = new List<ClassItem>();

        [BindProperty]
        public int SelectedClassId { get; set; }

        public string Message { get; set; }
        public string MessageType { get; set; } 

        public void OnGet()
        {
            LoadClasses();
        }

        public void OnPost()
        {
            if (SelectedClassId == 0)
            {
                Message = "Please select a class to join.";
                MessageType = "danger";
                LoadClasses();
                return;
            }

            try
            {
                using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    con.Open();

                    

                    string query = @"INSERT INTO Class_Enrollment (Class_ID, Member_ID, Trainer_ID, Enrollment_Date)
                                     SELECT Class_ID, 1, Trainer_ID, GETDATE()
                                     FROM Class
                                     WHERE Class_ID = @ClassId";

                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@ClassId", SelectedClassId);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Message = "Successfully enrolled in the class!";
                            MessageType = "success";
                        }
                        else
                        {
                            Message = "Error: Class not found.";
                            MessageType = "danger";
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
               
                Message = "Error booking class: " + ex.Message;
                MessageType = "danger";
            }

            
            LoadClasses();
        }

        private void LoadClasses()
        {
            Classes = new List<ClassItem>(); 
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                
                string sql = "SELECT Class_ID, Class_Name, Date, Start_Time FROM Class";
                using (SqlCommand cmd = new SqlCommand(sql, con))
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
                                StartTime = reader.GetTimeSpan(3).ToString(@"hh\:mm")
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
            public string StartTime { get; set; }
        }
    }
}