using Autopark.Controllers;
using Autopark.Data;
using Autopark.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using NuGet.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Autopark.Pages.Authentication
{
    public class AuthorizationModel(ApplicationDbContext _db, UserManager<AppUser> _userManager) : PageModel
    {
        private readonly ApplicationDbContext db = _db;
        private readonly UserManager<AppUser> userManager = _userManager;

        [BindProperty]
        public string Username { get; set; } = "";
        [BindProperty]
        public string Password { get; set; } = "";
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl)
        {
            var user = _db.Users.Where(u => u.UserName == Username).FirstOrDefault();

            if (user == null || !await userManager.CheckPasswordAsync(user, Password)) return Page();

            var userRoles = await userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ByYM000OLlMQG6VVVp1OH7Xzyr7gHuw1qvUC5dcGt3SNM"));

            var token = new JwtSecurityToken(
                issuer: "Sample",
                audience: "Sample",
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            HttpContext.Response.Cookies.Append(".Autopark.Nugget",
                new JwtSecurityTokenHandler().WriteToken(token),
                new CookieOptions
                {
                    MaxAge = TimeSpan.FromMinutes(60)
                });

            return Redirect("/");
        }
    }
}
