using Microsoft.AspNetCore.Identity;

namespace schoolMvc.DAL.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }
        public string City { get; set; }
        public int Age { get; set; }
    }
}
