namespace backend_app.Models
{
    public class AdminBallot
    {
        public string Name { get; set; }

        public string Winner { get; set; }

        public AdminItem[] Items { get; set; }

        public int Id { get; set; }
    }
}
