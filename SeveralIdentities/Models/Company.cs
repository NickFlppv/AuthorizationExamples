using Microsoft.AspNetCore.Identity;

namespace SeveralIdentities.Models
{
    public class Company : IdentityUser
    {
        public string Sphere { get; set; }
        public string Description { get; set; }
    }
}