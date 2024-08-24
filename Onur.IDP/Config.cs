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
            new IdentityResource("roles",
                "Your role(s)",
                ["role"]),
            new IdentityResource("country",
                "The country you' re living in",
                ["country"])
        };

    public static IEnumerable<ApiResource> ApiResources =>
        new ApiResource[]
            {
                new ApiResource("cryptomarketapi"
                    , "Crypto Market API"
                    , ["role", "country"])
                {
                    Scopes = { "cryptomarketapi.fullaccess",
                        "cryptomarketapi.read",
                        "cryptomarketapi.write" },
                    //ApiSecrets = { new Secret("apisecret".Sha256()) }
                }
            };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
            {
                new ApiScope("cryptomarketapi.fullaccess"),
                new ApiScope("cryptomarketapi.read"),
                new ApiScope("cryptomarketapi.write")
            };

    public static IEnumerable<Client> Clients =>
        new Client[]
            {
                new Client()
                {
                    ClientName = "CryptoMarket",
                    ClientId   = "cryptomarketclient",
                    AllowedGrantTypes = GrantTypes.Code,
                    //AccessTokenType = AccessTokenType.Reference,
                    //IdentityTokenLifetime = 300,
                    //AuthorizationCodeLifetime = 300,
                    AccessTokenLifetime = TimeSpan.FromMinutes(2).Seconds,
                    AllowOfflineAccess = true,
                    //AbsoluteRefreshTokenLifetime = 0,
                    //SlidingRefreshTokenLifetime = 60,                    
                    UpdateAccessTokenClaimsOnRefresh = true,

                    RedirectUris =
                    {
                        "https://localhost:7068/signin-oidc",
                    },
                    PostLogoutRedirectUris =
                    {
                        "https://localhost:7068/signout-callback-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "roles",
                        //"cryptomarketapi.fullaccess",
                        "cryptomarketapi.read",
                        "cryptomarketapi.write",
                        "country"
                    },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    RequireConsent = true
                },
                new Client()
                {
                    ClientName = "CryptoMarketSwaggerApi",
                    ClientId   = "cryptomarketswaggerapi",
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris =
                    {
                        "https://localhost:7068/swagger/oauth2-redirect.html"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        //"cryptomarketapi.fullaccess",
                        "cryptomarketapi.read",
                        "cryptomarketapi.write",
                        "country"
                    },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    RequireConsent = true
                }
            };
}