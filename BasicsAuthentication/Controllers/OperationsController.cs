using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace BasicsAuthentication.Controllers
{
    public class OperationsController : Controller
    {
        private readonly IAuthorizationService _authorizationService;

        public OperationsController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }
        
        public async Task<IActionResult> Open()
        {
            var requirement = new OperationAuthorizationRequirement
            {
                Name = CookieJarOperations.ComeNear
            };
            
            await _authorizationService.AuthorizeAsync(User, null, requirement);
            
            return View();
        }
        
    }
}