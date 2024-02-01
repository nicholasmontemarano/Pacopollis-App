using backend_app.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace backend_app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VoteViewController : ControllerBase, IVoteViewController
    {
        private int user;
        private int election;
        private readonly ILogger<LoginController> _logger;

        public VoteViewController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        public Ballot[] VoteView(int voter_id, int election_id)
        {
            user = voter_id;
            election = election_id;
            return BallotAccessor().ToArray();
        }

        public List<Ballot> BallotAccessor()
        {
            List<Ballot> electionsBallots = new List<Ballot>();
            
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
                            Ballot ballot = new Ballot();
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
            electionsBallots = ItemAccessor(electionsBallots);
            return electionsBallots;
        }
        
        public List<Ballot> ItemAccessor(List<Ballot> ballots) 
        {
            List<int> votes = AccessVotes(user, election); 

            foreach (Ballot ballot in ballots)
            {
                List<Item> items = new List<Item>();
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
                                Item itemStorage = new Item();
                                itemStorage.Party = reader.GetString(reader.GetOrdinal("party"));
                                itemStorage.Title = reader.GetString(reader.GetOrdinal("title"));
                                itemStorage.Id = reader.GetInt32(reader.GetOrdinal("item_id"));
                                if ((votes != null) & (votes.Contains(itemStorage.Id))) //if item_id is in list of votes
                                {
                                    itemStorage.VotedFor = true;
                                }
                                else
                                {
                                    itemStorage.VotedFor = false;
                                }
                                items.Add(itemStorage);
                            }
                        }
                    }
                }
                ballot.Items = items.ToArray();
            }
            return ballots;
        }

        public List<int> AccessVotes(int user_id, int election_id)
        {
            string sqlQuery = "SELECT votes FROM vote_info WHERE voter_id = @Voter_id AND election_id = @Election_id";
            string votesString = "";

            using (SqlConnection conn = SQLConnection.TestDatabaseConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    cmd.Parameters.Add("@Voter_id", SqlDbType.Int).Value = user_id;
                    cmd.Parameters.Add("@Election_id", SqlDbType.Int).Value = election_id;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            votesString = reader.GetString(reader.GetOrdinal("votes"));
                        }
                    }
                }
            }
            if (votesString != "")
            {
                List<int> votes = votesString.Split(',').Select(int.Parse).ToList();
                return votes;
            }
            else
            {
                List<int> noVotes = new List<int>();
                return noVotes;
            }
        }
    }
}
