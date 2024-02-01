using backend_app.Controllers;
using backend_app.Models;

namespace UnitTestProject
{
    [TestClass]
    public class ControllerAccessorTests
    {
        [TestMethod]
        public void TestIfVoter1Exists()
        {
            var test = new LoginController(null);
            Voter result = test.AuthenticateUser("Matthew", "Donsig", 1234, "2003-04-08", "password");
            Assert.AreEqual(1, result.UserID);
        }

        [TestMethod]
        public void TestIfVoterDoesNotExist()
        {
            var test = new LoginController(null);
            Voter result = test.AuthenticateUser("Matt", "Donsig", 1234, "2003-04-08", "pass");
            Assert.AreEqual(-1, result.UserID);
        }

        [TestMethod]
        public void TestFirstBallotAccessor()
        {
            var test = new VoteViewController(null);
            Ballot[] ballots = test.VoteView(1, 1);
            Assert.AreEqual(1 , ballots[0].Id);
            Assert.AreEqual("Mayor", ballots[0].Name);
        }

        [TestMethod]
        public void TestSecondBallotAccessor()
        {
            var test = new VoteViewController(null);
            Ballot[] ballots = test.VoteView(1, 1);
            Assert.AreEqual(2 , ballots[1].Id);
            Assert.AreEqual("Issue 21", ballots[1].Name);
        }

        [TestMethod]
        public void TestFirstAdminBallotAccessor()
        {
            var test = new AdminViewController(null);
            AdminBallot[] ballots = test.AdminView(1);
            Assert.AreEqual(1, ballots[0].Id);
            Assert.AreEqual("Mayor", ballots[0].Name);
            Assert.AreEqual(1, ballots[0].Items[0].AdminVoteTotal);
            Assert.AreEqual(1, ballots[0].Items[1].AdminVoteTotal);
        }

        [TestMethod]
        public void TestSecondAdminBallotAccessor()
        {
            var test = new AdminViewController(null);
            AdminBallot[] ballots = test.AdminView(1);
            Assert.AreEqual(2, ballots[1].Id);
            Assert.AreEqual("Issue 21", ballots[1].Name);
            Assert.AreEqual(2, ballots[1].Items[0].AdminVoteTotal);
            Assert.AreEqual(0, ballots[1].Items[1].AdminVoteTotal);
        }

        [TestMethod]
        public void TestIfVotedDoesExist()
        {
            var test = new IfVotedController(null);
            bool vote = test.IfVoted(1, 1);
            Assert.IsTrue(vote);
        }

        [TestMethod]
        public void TestIfVotedDoesNotExist()
        {
            var test = new IfVotedController(null);
            bool vote = test.IfVoted(2, 1);
            Assert.IsFalse(vote);
        }

        [TestMethod]
        public void TestSuccessfulRetrievalOfElections()
        {
            var controller = new ElectionController(null);
            List<Election> result = controller.FindAllElections();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);
        }

        [TestMethod]
        public void TestRetrievalWithNoElections()
        {
            var controller = new ElectionController(null);
            List<Election> result = controller.FindAllElections();
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void TestWithEmptyInputs()
        {
            var controller = new LoginController(null);
            Voter result = controller.AuthenticateUser("", "", 0, "", "");
            Assert.AreEqual(-1, result.UserID);
        }

        [TestMethod]
        public void TestWithIncorrectPassword()
        {
            var controller = new LoginController(null);
            Voter result = controller.AuthenticateUser("Matthew", "Donsig", 1234, "2003-04-08", "wrongpassword");
            Assert.AreEqual(-1, result.UserID);
        }
    }
}