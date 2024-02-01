namespace backend_app.Models
{
    public class Ballot
    {
        public string Name { get; set; }

        public string Winner { get; set; }

        public Item[] Items { get; set; }

        public int Id { get; set; } 
    }
}
