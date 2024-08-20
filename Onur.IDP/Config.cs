using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Onur.IDP;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource("roles"
                ,"Your role(s)"
                ,["role"])
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
            { };

    public static IEnumerable<Client> Clients =>
        new Client[]
            {
                new Client()
                {
                    ClientName = "CryptoMarket",
                    ClientId   = "cryptomarketclient",
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris =
                    {
                        "https://localhost:7068/signin-oidc"
                    },
                    PostLogoutRedirectUris =
                    {
                        "https://localhost:7068/signout-callback-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "roles"
                    },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    RequireConsent = true
                }
            };
}