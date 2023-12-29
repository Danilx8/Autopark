using Microsoft.AspNetCore.Identity;

namespace Autopark.Models
{
    public class AppUser : IdentityUser
    {
        public List<EnterpriseManager>? ManagedCompanies { get; set; }
    }
}
