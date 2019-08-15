using System.Collections.Generic;
using IdentityModel;
using IdentityServer4.Models;

namespace CloudMe.Auth.Admin.Configuration.IdentityServer
{
    public class ClientResources
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new[]
            {
                // some standard scopes from the OIDC spec
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResources.Email(),

                // custom identity resource with some consolidated claims
                new IdentityResource("custom.profile", new[] { JwtClaimTypes.Name, JwtClaimTypes.Email, "location" }),

                // add additional identity resource
                new IdentityResource("roles", "Roles", new[] { "role" }),
                new IdentityResource("country","Your country you're living in", new List<string>(){"country"}),
                new IdentityResource("subscriptionlevel","Your subscription level", new List<string>(){"subscriptionlevel"}),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
               {
                new ApiResource("ToDeTaxiapi","ToDeTaxi API")
                {
                    ApiSecrets = { new Secret("ToDeTaxiapisecret".Sha256())}
                },
            
                // simple version with ctor
                new ApiResource("api1", "Some API 1")
                                {
                    // this is needed for introspection when using reference tokens
                    ApiSecrets = { new Secret("secret".Sha256()) }
                                },
                
                // expanded version if more control is needed
                new ApiResource
                                {
                                        Name = "api2",

                                        ApiSecrets =
                                        {
                                                new Secret("secret".Sha256())
                                        },

                                        UserClaims =
                                        {
                                                JwtClaimTypes.Name,
                                                JwtClaimTypes.Email
                                        },

                                        Scopes =
                                        {
                                                new Scope()
                                                {
                                                        Name = "api2.full_access",
                                                        DisplayName = "Full access to API 2",
                                                },
                                                new Scope
                                                {
                                                        Name = "api2.read_only",
                                                        DisplayName = "Read only access to API 2"
                                                }
                                        }
                                }
                        };
        }
    }
}