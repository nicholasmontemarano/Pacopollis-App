namespace backend_app.Models
{
    public class Vote
    {
        /*
         * Can be used to insert votes, and also retreive certain votes from the database
         * due to the nullable VoteInfoID field. Only use with null is when you are putting
         * a casted vote into DB, otherwise this can be used as a struct to retrieve a persons
         * votes on an election.
         */
        public int VoterID { get; set; }
        public int ElectionID { get; set; }
        public string? Votes { get; set; }
    }
}
