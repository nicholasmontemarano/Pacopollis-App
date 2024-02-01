using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using backend_app.Models;

namespace backend_app.Controllers
{
    [ApiController]
    [Route("api/SendVotes")]
    public class VoteMutator : Controller, IVoteMutator
    {
        private readonly ILogger<VoteMutator> _logger;

        public VoteMutator(ILogger<VoteMutator> logger)
        {
            _logger = logger;

        }

        [HttpPost]

        public void SendVotes([FromQuery] int voter_id, [FromQuery] int election_id, [FromQuery] string votes)
        {
            Vote vote = new Vote();
            vote.VoterID = voter_id;
            vote.ElectionID = election_id;
            vote.Votes = votes;

            CastVotes(vote);
        }

        public void CastVotes(Vote castVotes)
        {
            string sqlQuery = "INSERT INTO vote_info (voter_id, election_id, votes) VALUES (@VoterId, @ElectionId, @Votes)";

            using (SqlConnection conn = SQLConnection.TestDatabaseConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    cmd.Parameters.Add("@VoterId", SqlDbType.Int).Value = castVotes.VoterID;
                    cmd.Parameters.Add("@ElectionId", SqlDbType.Int).Value = castVotes.ElectionID;
                    cmd.Parameters.Add("@Votes", SqlDbType.VarChar, 255).Value = castVotes.Votes;

                    cmd.ExecuteReader();
                    conn.Close();
                }
            }

        }

    }
}
