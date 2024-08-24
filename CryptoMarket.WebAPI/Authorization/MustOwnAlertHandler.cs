using Microsoft.AspNetCore.Authorization;

namespace CryptoMarket.WebAPI.Authorization
{
    public class MustOwnAlertHandler : AuthorizationHandler<MustOwnAlertRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public MustOwnAlertHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MustOwnAlertRequirement requirement)
        {
            var alertId = _httpContextAccessor.HttpContext?.GetRouteValue("id")?.ToString();

            if (!int.TryParse(alertId, out int alertIdAsInt))
            {
                context.Fail();
                return;
            }

            var ownerId = context.User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value;
            if (ownerId == null)
            {
                context.Fail();
                return;
            }

            // Check if the user owns the alert
            // this is a dummy check, replace it with your own logic
            if (alertId != ownerId)
            {
                context.Fail();
                return;
            }

            context.Succeed(requirement);
        }
    }
}
