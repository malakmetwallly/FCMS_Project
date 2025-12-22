using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq; 

namespace FCMS_Project_New.Pages.Classes 
{
    public class ScheduleModel : PageModel
    {
        private readonly string _connectionString;

        public ScheduleModel(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<ClassItem> WeeklyClasses { get; set; } = new List<ClassItem>();

        public void OnGet()
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();

                
                string query = @"
                    SELECT 
                        c.Class_Name, 
                        t.Name AS TrainerName,
                        LEFT(DATENAME(WEEKDAY, c.Date), 3) AS DayAbbrev, 
                        c.Start_Time
                    FROM Class c
                    JOIN Trainer t ON c.Trainer_ID = t.Trainer_ID
                    ORDER BY c.Start_Time";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            WeeklyClasses.Add(new ClassItem
                            {
                                Name = reader.GetString(0),     
                                Trainer = reader.GetString(1),  
                                Day = reader.GetString(2),     
                                
                                Time = reader.GetTimeSpan(3).ToString(@"hh\:mm")
                            });
                        }
                    }
                }
            }
        }

        public class ClassItem
        {
            public string Day { get; set; }
            public string Time { get; set; }
            public string Name { get; set; }
            public string Trainer { get; set; }
        }
    }
}