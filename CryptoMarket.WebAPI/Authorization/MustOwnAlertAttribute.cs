using Microsoft.AspNetCore.Authorization;

namespace CryptoMarket.WebAPI.Authorization
{
    public class MustOwnAlertAttribute : AuthorizeAttribute, IAuthorizationRequirementData
    {
        public IEnumerable<IAuthorizationRequirement> GetRequirements()
        {
            return [new MustOwnAlertRequirement()];
        }
    }
}
