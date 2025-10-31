using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRMDesktopUI.Library.Models
{
    public class LoggedInUserModels : ILoggedInUserModels
    {
        public string Token { get; set; }

        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
