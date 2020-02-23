using Microsoft.AspNetCore.Authorization;

namespace BasicsAuthentication.AuthorizationRequirements
{
    public static class AuthorizationPolicyBuilderExtensions
    {
        public static AuthorizationPolicyBuilder RequireCustomClaim(
            this AuthorizationPolicyBuilder builder, 
            string claimType
        )
        {
            builder.AddRequirements(new CustomRequireClaim(claimType));
            return builder;
        }
    }
}