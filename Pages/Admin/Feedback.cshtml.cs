using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Collections.Generic;
using System;

namespace FCMS_Project.Pages.Admin
{
    public class FeedbackModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public FeedbackModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public class FeedbackItem
        {
            public string MemberName { get; set; }
            public string Message { get; set; }
            public DateTime Date { get; set; }
        }

        public List<FeedbackItem> FeedbackList = new List<FeedbackItem>();

        public void OnGet()
        {
            string connectionString = _configuration.GetConnectionString("DefaultConnection");
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT M.Name, F.Message, F.Date_Submitted FROM Feedback F JOIN Member M ON F.Member_ID = M.Member_ID ORDER BY F.Date_Submitted DESC";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            FeedbackItem item = new FeedbackItem();
                            item.MemberName = reader.GetString(0);
                            item.Message = reader.GetString(1);
                            item.Date = reader.GetDateTime(2);
                            FeedbackList.Add(item);
                        }
                    }
                }
            }
        }
    }
}