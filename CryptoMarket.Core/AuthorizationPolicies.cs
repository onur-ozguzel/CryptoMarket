using Microsoft.AspNetCore.Authorization;

namespace CryptoMarket.Core
{
    public static class AuthorizationPolicies
    {
        public static AuthorizationPolicy CanAccessPremiumEndpoints()
        {
            return new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireClaim("country", "be")
                .RequireRole("PayingUser")
                .Build();
        }
    }
}
