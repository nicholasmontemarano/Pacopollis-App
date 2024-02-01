using Group9Project.Controllers.Accessors;

namespace TestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestIfVoter1Exists()
        {
            var test = new LoginAccessor();
            int? result = test.AuthenticateUser("Matthew", "Donsig", 1234, "2003-04-08", "password");
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void TestIfVoterDoesNotExist()
        {
            var test = new LoginAccessor();
            int? result = test.AuthenticateUser("Matt", "Donsig", 1234, "2003-04-08", "pass");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TestIfVoterIsAdmin()
        {
            var test = new AdminAccessor();
            bool result = test.CheckIfAdmin(4);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestIfVoterIsNotAdmin()
        {
            var test = new AdminAccessor();
            bool result = test.CheckIfAdmin(1);
            Assert.IsFalse(result);
        }
    }
}