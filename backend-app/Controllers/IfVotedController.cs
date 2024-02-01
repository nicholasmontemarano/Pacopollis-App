using System;
using System.Data;
using System.Data.SqlClient;
using backend_app.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend_app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IfVotedController : ControllerBase, IIfVotedController
    {
        private int user;
        private int election;
        private readonly ILogger<LoginController> _logger;

        public IfVotedController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public bool IfVoted(int voter_id, int election_id)
        {
            user = voter_id;
            election = election_id;
            return IfVotedAccessor();
        }

        public bool IfVotedAccessor()
        {
            string sqlQuery = "SELECT COUNT(*) AS bool FROM vote_info WHERE election_id = @Election_id AND voter_id = @Voter_id";

            using (SqlConnection conn = SQLConnection.TestDatabaseConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    cmd.Parameters.Add("@Election_id", SqlDbType.Int).Value = election;
                    cmd.Parameters.Add("@Voter_id", SqlDbType.Int).Value = user;
                    int count = (int) cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        return true;
                    } else
                    {
                        return false;
                    }
                }
            }
        }
    }
}
