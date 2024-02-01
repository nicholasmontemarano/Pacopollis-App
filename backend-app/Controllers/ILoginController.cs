using backend_app.Models;
using System;

namespace backend_app.Controllers
{
    public interface ILoginController
    {
        Voter Login(string firstName, string lastName, int SSN, string birthday, string password);
        Voter AuthenticateUser(string firstName, string lastName, int SSN, string birthday, string password);
    }
}