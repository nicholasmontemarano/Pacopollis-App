using System.Data;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.X86;
using backend_app.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Collections.Specialized.BitVector32;

namespace backend_app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminViewController : ControllerBase, IAdminViewController
    {
        private int election;
        private readonly ILogger<LoginController> _logger;

        public AdminViewController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public AdminBallot[] AdminView(int election_id)
        {
            election = election_id;
            return AdminBallotAccessor().ToArray();
        }

        public List<AdminBallot> AdminBallotAccessor()
        {
            List<AdminBallot> electionsBallots = new List<AdminBallot>();

            string sqlQuery = "SELECT ballot_item_id, issue, victory_id FROM ballot_item WHERE election_id = @Election_id";

            using (SqlConnection conn = SQLConnection.TestDatabaseConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    cmd.Parameters.Add("@Election_id", SqlDbType.Int).Value = election;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            AdminBallot ballot = new AdminBallot();
                            ballot.Id = reader.GetInt32(reader.GetOrdinal("ballot_item_id"));
                            ballot.Name = reader.GetString(reader.GetOrdinal("issue"));
                            if (!reader.IsDBNull(reader.GetOrdinal("victory_id")))
                            {
                                int win_id = reader.GetInt32(reader.GetOrdinal("victory_id"));
                                string sqlQueryTwo = "SELECT title FROM item WHERE item_id = @Victory_id";
                                using (SqlCommand cmdTwo = new SqlCommand(sqlQueryTwo, conn))
                                {
                                    cmdTwo.Parameters.Add("@Victory_id", SqlDbType.Int).Value = win_id;
                                    using (SqlDataReader readerTwo = cmdTwo.ExecuteReader())
                                    {
                                        ballot.Winner = readerTwo.GetString(readerTwo.GetOrdinal("title"));
                                    }
                                }
                            }
                            electionsBallots.Add(ballot);
                        }
                    }
                }
            }
            electionsBallots = AdminItemAccessor(electionsBallots);
            return electionsBallots;
        }

        public List<AdminBallot> AdminItemAccessor(List<AdminBallot> ballots)
        {
            foreach (AdminBallot ballot in ballots)
            {
                List<AdminItem> items = new List<AdminItem>();
                string sqlQuery = "SELECT title, party, item_id FROM item WHERE ballot_id = @Ballot_id";

                using (SqlConnection conn = SQLConnection.TestDatabaseConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                    {
                        cmd.Parameters.Add("@Ballot_id", SqlDbType.Int).Value = ballot.Id;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                AdminItem itemStorage = new AdminItem();
                                itemStorage.Party = reader.GetString(reader.GetOrdinal("party"));
                                itemStorage.Title = reader.GetString(reader.GetOrdinal("title"));
                                itemStorage.Id = reader.GetInt32(reader.GetOrdinal("item_id"));
                                items.Add(itemStorage);
                            }
                        }
                    }
                }
                items = VoteCounter(items);
                ballot.Items = items.ToArray();
            }
            return ballots;
        }
        public List<AdminItem> VoteCounter(List<AdminItem> items)
        {
            foreach (AdminItem item in items)
            {
                string votesString = "";
                int count = 0;
                string sqlQuery = "SELECT votes FROM vote_info";

                using (SqlConnection conn = SQLConnection.TestDatabaseConnection())
                {
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                votesString = reader.GetString(reader.GetOrdinal("votes"));
                                List<int> votes = votesString.Split(',').Select(int.Parse).ToList();

                                if (votes.Contains(item.Id))
                                {
                                    count++;
                                }
                            }
                        }
                    }
                }
                item.AdminVoteTotal = count;
            }
            return items;
        }
    }
}