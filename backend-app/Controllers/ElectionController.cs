using System.Data;
using System.Data.SqlClient;
using System.Runtime.Intrinsics.X86;
using backend_app.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Collections.Specialized.BitVector32;

namespace backend_app.Controllers
{
    [ApiController]
    [Route("api/FindAllElections")]
    public class ElectionController : ControllerBase, IElectionController
    {
        private readonly ILogger<ElectionController> _logger;

        public ElectionController(ILogger<ElectionController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Election> FindAllElections()
        {
            const string sqlQuery = "SELECT * FROM election";
            var electionList = new List<Election>();

            try
            {
                using (var conn = SQLConnection.TestDatabaseConnection())
                {
                    using (var cmd = new SqlCommand(sqlQuery, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var election = new Election
                                {
                                    ElectionId = reader.GetInt32(reader.GetOrdinal("election_id")),
                                    StartDate = reader.GetDateTime(reader.GetOrdinal("start_date")),
                                    EndDate = reader.GetDateTime(reader.GetOrdinal("end_date"))
                                };
                                electionList.Add(election);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return electionList;
        }
    }
}