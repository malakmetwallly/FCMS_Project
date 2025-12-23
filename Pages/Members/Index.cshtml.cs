using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;

namespace FCMS_Project.Pages.Members
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public List<Member> Members { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int? SearchId { get; set; }

        public void OnGet()
        {
            string cs = _configuration.GetConnectionString("DefaultConnection");

            using SqlConnection conn = new SqlConnection(cs);
            conn.Open();

            string sql = @"
                SELECT Member_ID, Name, Age, Goal, Membership_Type
                FROM Member
            ";

            if (SearchId.HasValue)
            {
                sql += " WHERE Member_ID = @id";
            }

            using SqlCommand cmd = new SqlCommand(sql, conn);

            if (SearchId.HasValue)
            {
                cmd.Parameters.AddWithValue("@id", SearchId.Value);
            }

            using SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Members.Add(new Member
                {
                    Member_ID = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Age = reader.GetInt32(2),
                    Goal = reader.GetString(3),
                    Membership_Type = reader.GetString(4)
                });
            }
        }
    }
}
