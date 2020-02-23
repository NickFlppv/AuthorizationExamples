using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace BasicsAuthentication.Transformer
{
    //it has to be added in startup                 
    public class ClaimsTransformation : IClaimsTransformation
    {
        //magic that can help populate principal with claims
        //during authentication session
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var hasFriendClaim = principal.Claims.Any(x => x.Type == "Friend");
            if (!hasFriendClaim)
            {
                ((ClaimsIdentity) principal.Identity).AddClaim(new Claim("Friend", "Bad"));
            }

            return Task.FromResult(principal);
        }
    }
}