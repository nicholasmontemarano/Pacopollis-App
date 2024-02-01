using System.Data.SqlClient;

namespace backend_app
{
    class SQLConnection
    {
        public static SqlConnection TestDatabaseConnection()
        {
            string connString = "Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;";

            SqlConnection conn = new SqlConnection(connString);
            try
            {
                conn.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            return conn;
        }
    }
}