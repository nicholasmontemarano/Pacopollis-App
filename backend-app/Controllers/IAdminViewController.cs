using backend_app.Models;

namespace backend_app.Controllers
{
    public interface IAdminViewController
    {
        AdminBallot[] AdminView(int election_id);
        List<AdminBallot> AdminBallotAccessor();
        List<AdminBallot> AdminItemAccessor(List<AdminBallot> ballots);
        List<AdminItem> VoteCounter(List<AdminItem> items);

    }
}
