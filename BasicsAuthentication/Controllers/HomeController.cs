using System;
using System.Collections.Generic;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BasicsAuthentication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }
        
        [Authorize(Policy = "Claim.DoB")]
        public IActionResult SecretPolicy()
        {
            return View();
        }

        public IActionResult Authenticate()
        {
            var grandmaClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Bob"),
                new Claim(ClaimTypes.DateOfBirth, "29/04/1999"),
                new Claim(ClaimTypes.Email, "Bob@email.com"),
                new Claim("Grandma.says", "Very nice boy.")
            };
            var licenseClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, "email@email.com"),
                new Claim("DrivingLicense", "A")
            };
            var grandmaIdentity = new ClaimsIdentity(grandmaClaims, "Grandma Identity");
            var licenseIdentity = new ClaimsIdentity(licenseClaims, "Police");
            var userPrincipal = new ClaimsPrincipal(new[] {grandmaIdentity, licenseIdentity});

            HttpContext.SignInAsync(userPrincipal);

            return RedirectToAction("Index");
        }
    }
}