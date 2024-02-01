namespace backend_app.Controllers
{
    public interface IIfVotedController
    {
        bool IfVoted(int voter_id, int election_id);
        bool IfVotedAccessor();
    }
}
