using CryptoMarket.Business;
using CryptoMarket.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.OpenApi.Models;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "CryptoMarket.WebAPI", Version = "v1" });

    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,        
        Flows = new OpenApiOAuthFlows
        {
            AuthorizationCode = new OpenApiOAuthFlow
            {
                AuthorizationUrl = new Uri("https://localhost:5001/connect/authorize"),
                TokenUrl = new Uri("https://localhost:5001/connect/token"),
                Scopes = new Dictionary<string, string>
                    {
                        { "openid", "OpenID Connect scope" },
                        { "cryptomarketapi.fullaccess", "Access to API" }
                    }
            },
        }
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "oauth2"
                    }
                },
                new[] { "openid", "cryptomarketapi.fullaccess" }
            }
        });
});
builder.Services.AddHttpContextAccessor();

builder.Services.AddBusinessServices(builder.Configuration);
builder.Services.AddCoreServices();

// sample usage of rate limiting
builder.Services.AddRateLimiter(_ => _
    .AddFixedWindowLimiter(policyName: "fixed", options =>
    {
        options.PermitLimit = 4;
        options.Window = TimeSpan.FromSeconds(12);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 2;
    }));

JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:5001";
        options.Audience = "cryptomarketapi";
        options.TokenValidationParameters = new()
        {
            NameClaimType = "given_name",
            RoleClaimType = "role",
            ValidTypes = ["at+jwt"]
        };
    });

var app = builder.Build();

app.ValidateCoinMarketCapConfig();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "CryptoMarket.WebAPI V1");
    options.OAuthClientId("cryptomarketapi");
    options.OAuthClientSecret("secret");
    options.OAuthAppName("Swagger UI - CryptoMarket.WebAPI");
    options.OAuthUsePkce();
    options.OAuth2RedirectUrl("https://localhost:7068/swagger/oauth2-redirect.html");
    options.OAuthScopes("openid", "cryptomarketapi.fullaccess");
});
//}

//if (app.Environment.IsProduction())
//{
app.UseExceptionMiddleware();
//}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers().RequireRateLimiting("fixed");

app.UseRateLimiter();

app.Run();
