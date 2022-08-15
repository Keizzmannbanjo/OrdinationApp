using Microsoft.AspNetCore.Identity;

namespace OrdinationApp.Models
{
    public class TrackerUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string LastName { get; set; }

        public string Rank { get; set; }

        public string Province { get; set; }
    }
}
