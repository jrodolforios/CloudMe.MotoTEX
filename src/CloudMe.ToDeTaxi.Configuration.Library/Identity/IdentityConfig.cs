using IdentityServer4.Models;
using System.Collections.Generic;

namespace CloudMe.ToDeTaxi.Configuration.Library.Identity
{
    public class IdentityConfig
    {
        public static string masterUserName = "cloudme";
        public static string masterUserPassword = "Cloudme@123";
        
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
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                // resource owner password grant client
                new Client
                {
                    ClientId = "ToDeTaxiAPI",
                    ClientName = "Cliente do sistema TOdeTaxi",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        // TODO: Alterar Secret!
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "todetaxiapi" },
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,
                    RedirectUris = {
                        "https://localhost:4200/assets/login-callback.html",
                        "https://localhost:4200/assets/silent-renew.html"
                    },
                    PostLogoutRedirectUris = {
                        "https://localhost:4200"
                    }
                },
                new Client
                {
                    ClientId = "ToDeTaxiAPI_swagger",
                    ClientName = "API swagger do sistema TOdeTaxi",
                    AllowedScopes = {"todetaxiapi"},
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = {
                        "https://localhost:5000/swagger/oauth2-redirect.html",
                        "https://localhost:5000/swagger/o2c.html"
                    },
                }
            };
        }
    }
}
