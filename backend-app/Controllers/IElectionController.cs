using backend_app.Models;
using System.Collections.Generic;

namespace backend_app.Controllers
{
    public interface IElectionController
    {
        List<Election> FindAllElections();
    }
}