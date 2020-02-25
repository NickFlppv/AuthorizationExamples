using Microsoft.AspNetCore.Identity;

namespace SeveralIdentities.Models
{
    public class Student : IdentityUser
    {
        public string University { get; set; }
        public string Course { get; set; }
    }
}