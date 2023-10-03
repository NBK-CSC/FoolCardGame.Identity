using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace FoolCardGame.Identity;

public static class Configuration
{
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>()
        {
            new ApiScope("TEST", "DISPLAY TEST NAME")
        };

    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>()
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };
    
    public static IEnumerable<ApiResource> ApiResources =>
        new List<ApiResource>()
        {
            new ApiResource("TEST", "TEST DISPLAY NAME", new []
            { JwtClaimTypes.Name })
            {
                Scopes = {"Test"}
            }
        };
    
    public static IEnumerable<Client> Clients =>
        new List<Client>()
        {
            new Client()
            {
                ClientId = "foolcardgame-web-api",
                ClientName = "FoolCardGame Web",
                AllowedGrantTypes = GrantTypes.Code,
                RequireClientSecret = false,
                RequirePkce = true,
                RedirectUris =
                {
                    "http://.../signin-oidc"
                },
                AllowedCorsOrigins =
                {
                    "http:// ..."
                },
                PostLogoutRedirectUris =
                {
                    "http:// .../signout-oids"
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "FoolCardGameWebAPI"
                },
                AllowAccessTokensViaBrowser = true
            }
        };
}