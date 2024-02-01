using backend_app.Models;

namespace backend_app.Controllers
{
    public interface IVoteViewController
    {
        Ballot[] VoteView(int voter_id, int election_id);
        List<Ballot> BallotAccessor();
        List<Ballot> ItemAccessor(List<Ballot> ballots);
        List<int> AccessVotes(int user_id, int election_id);
    }
}
