using Autopark.Controllers;
using Autopark.Data;
using Autopark.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Autopark.Pages.Authentication
{
    public class AuthorizationModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public AuthorizationModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
        }

        public ActionResult OnPost(string username, string password, string? returnUrl)
        {
            PasswordHasher<AppUser> hasher = new();
            var manager = _db.Users.FirstOrDefault(m => m.UserName == username);

            if (manager is null) return BadRequest();

            PasswordVerificationResult isPasswordRight = hasher.VerifyHashedPassword(manager, manager.PasswordHash, password);

            if (!isPasswordRight.HasFlag(PasswordVerificationResult.Success)) return BadRequest();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, manager.UserName),
                new Claim(ClaimTypes.Role, "manager")
            };

            // создаем объект ClaimsIdentity
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
            ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
            return Redirect(returnUrl ?? "/");
        }
    }
}
