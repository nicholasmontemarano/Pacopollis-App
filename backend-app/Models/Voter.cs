using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace backend_app.Models
{
    public class Voter
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SSN { get; set; }
        public string Birthday { get; set; }
        public string Password { get; set; }
    }
}