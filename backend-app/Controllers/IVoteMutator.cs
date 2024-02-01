using backend_app.Models;

namespace backend_app.Controllers
{
    public interface IVoteMutator
    {
        void SendVotes(int voterId, int electionId, string votes);
        void CastVotes(Vote castVotes);
    }
}