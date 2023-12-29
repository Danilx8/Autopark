using Microsoft.AspNetCore.Identity;

namespace Autopark.Models.Roles
{
    public class Manager : IdentityRole
    {
        public Manager() : base("manager")
        {
        }
    }
}
