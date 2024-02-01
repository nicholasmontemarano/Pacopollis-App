using System.Data;
using System.Data.SqlClient;
using backend_app.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend_app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IsAdminController : ControllerBase, IIsAdminController
    {
        private readonly ILogger<IsAdminController> _logger;

        public IsAdminController(ILogger<IsAdminController> logger)
        {
            _logger = logger;

        }

        [HttpGet]
        public bool IsAdmin(int voterId)
        {
            bool isAdmin = false;
            string sqlQuery = "SELECT admin_check FROM voter WHERE voter_id = @VoterId";

            using (SqlConnection conn = SQLConnection.TestDatabaseConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    cmd.Parameters.Add("@VoterId", SqlDbType.Int).Value = voterId;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isAdmin = reader.GetBoolean(reader.GetOrdinal("admin_check"));
                        }
                    }

                    conn.Close();
                }
            }

            return isAdmin;
        }
    }
}
