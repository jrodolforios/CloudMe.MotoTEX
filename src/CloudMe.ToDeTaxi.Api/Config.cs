using System.Collections.Generic;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using static IdentityServer4.IdentityServerConstants;

public class Config
{
    public static IEnumerable<IdentityResource> GetIdentityResources()
    {
        return new List<IdentityResource>
        {
            new IdentityResources.OpenId()
        };
    }

    // scopes define the API resources in your system
    public static IEnumerable<ApiResource> GetApiResources()
    {
        return new List<ApiResource>
        {
            new ApiResource("todetaxiapi", "ToDeTaxiAPI")
        };
    }

    // client want to access resources (aka scopes)
    public static IEnumerable<Client> GetClients(IConfiguration Configuration)
    {
        // client credentials client
        return new List<Client>
        {
            // resource owner password grant client
            new Client
            {
                ClientId = Configuration.GetValue<string>("Identity:Clients:ToDeTaxiAPI:ID"),
                ClientName = Configuration.GetValue<string>("Identity:Clients:ToDeTaxiAPI:Name"),
                AllowedGrantTypes = GrantTypes.Implicit,
                ClientSecrets =
                {
                    // TODO: Alterar Secret!
                    new Secret("secret".Sha256())
                },
                AllowedScopes = { "todetaxiapi", StandardScopes.OpenId },
                AllowAccessTokensViaBrowser = true,
                RequireConsent = false,
                RedirectUris = Configuration.GetSection("Identity:Clients:ToDeTaxiAPI:RedirectUris").Get<string[]>(),
                PostLogoutRedirectUris = Configuration.GetSection("Identity:Clients:ToDeTaxiAPI:PostLogoutRedirectUris").Get<string[]>()
            },
            new Client
            {
                ClientId = Configuration.GetValue<string>("Identity:Clients:ToDeTaxiAPI_swagger:ID"),
                ClientName = Configuration.GetValue<string>("Identity:Clients:ToDeTaxiAPI_swagger:Name"),
                AllowedScopes = {"todetaxiapi"},
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowAccessTokensViaBrowser = true,
                AllowedCorsOrigins = new List<string>
                {
                    Configuration.GetValue<string>("Identity:Authority")
                },
                RedirectUris = Configuration.GetSection("Identity:Clients:ToDeTaxiAPI_swagger:RedirectUris").Get<string[]>(),
            }
        };
    }
}