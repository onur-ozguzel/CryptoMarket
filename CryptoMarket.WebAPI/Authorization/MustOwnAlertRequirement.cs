using Microsoft.AspNetCore.Authorization;

namespace CryptoMarket.WebAPI.Authorization
{
    public class MustOwnAlertRequirement: IAuthorizationRequirement
    {
        public MustOwnAlertRequirement()
        {
            
        }
    }
}
