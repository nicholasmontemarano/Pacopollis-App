using System.Data;
using System.Data.SqlClient;
using backend_app.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend_app.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase, ILoginController
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;

        }

        [HttpGet]
        public Voter Login( string FirstName, string LastName, int SSN, string Birthday, string Password)
        {
            return AuthenticateUser(FirstName, LastName, SSN,Birthday, Password);
        }

        public Voter AuthenticateUser(string firstName, string lastName, int SSN, string birthday, string password)
        {
            int voterId;
            Voter voter = new Voter();

            string sqlQuery = "SELECT voter_id FROM voter WHERE first_name = @FirstName AND last_name = @LastName AND ss_number = @SSN AND birthday = @Birthday AND password = @Password";

            using (SqlConnection conn = SQLConnection.TestDatabaseConnection())
            {
                using (SqlCommand cmd = new SqlCommand(sqlQuery, conn))
                {
                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 255).Value = firstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 255).Value = lastName;
                    cmd.Parameters.Add("@SSN", SqlDbType.Int).Value = SSN;
                    cmd.Parameters.Add("@Birthday", SqlDbType.Date).Value = DateTime.Parse(birthday);
                    cmd.Parameters.Add("@Password", SqlDbType.VarChar, 255).Value = password;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            voterId = reader.GetInt32(reader.GetOrdinal("voter_id"));
                        }
                        else
                        {
                            voterId = -1; 
                        }
                    }
                    conn.Close();
                }
            }
            voter.UserID = voterId;
            voter.FirstName = firstName;
            voter.LastName = lastName;
            voter.Password = password;
            voter.Birthday = birthday;
            voter.SSN = SSN;
            return voter;
        }

        
    }
}
