using Microsoft.AspNetCore.Identity;

namespace HDD.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string VIN { get; set; }
        public string Plate { get; set; }
        public string RegisteredZip { get; set; }
    }
}