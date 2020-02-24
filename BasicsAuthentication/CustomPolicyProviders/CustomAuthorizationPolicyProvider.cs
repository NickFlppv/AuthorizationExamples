using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace BasicsAuthentication.CustomPolicyProviders
{
    public static class DynamicPolicies
    {
        public static IEnumerable<string> Get()
        {
            yield return SecurityLevel;
            yield return Rank;
        }
        
        public const string SecurityLevel = "SecurityLevel";
        public const string Rank = "Rank";
    }
    
    // it has to be added to startup class as service
    public class CustomAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        public CustomAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
        {
        }
        
        public override Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            foreach (var customPolicy in DynamicPolicies.Get())
            {
                if (policyName.StartsWith(customPolicy))
                {
                    // i can use Factory method to create a policy based on policyName here
                    // policyName is a value of AuthorizeAttribute`s Name
                   var policy = new AuthorizationPolicyBuilder().Build();

                   return Task.FromResult(policy);
                }
            }
            return base.GetPolicyAsync(policyName);
        }

    }
}