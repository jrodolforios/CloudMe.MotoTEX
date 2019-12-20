using System.Collections.Generic;
using IdentityModel;
using IdentityServer4.Models;

namespace CloudMe.Auth.Admin.Configuration.IdentityServer
{
    public class ClientResources
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                // some standard scopes from the OIDC spec
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResources.Email(),
                new IdentityResource("roles", "Roles", new[] { "role" }),
                /*
				// custom identity resource with some consolidated claims
                new IdentityResource("custom.profile", new[] { JwtClaimTypes.Name, JwtClaimTypes.Email, "location" }),

                // add additional identity resource
                new IdentityResource("roles", "Roles", new[] { "role" }),
                new IdentityResource("country","Your country you're living in", new List<string>(){"country"}),
                new IdentityResource("subscriptionlevel","Your subscription level", new List<string>(){"subscriptionlevel"}),*/
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
			{
                new ApiResource("todetaxiapi", "ToDeTaxiAPI")
                /*new ApiResource("callcenterapi","CallCenter API")
                {
                    ApiSecrets = { new Secret("callcenterapisecret".Sha256())}
                },*/,
                /*new ApiResource
                {
                    Name = adminConfiguration.IdentityAdminApiScope,
                    Scopes = new List<Scope>
                    {
                        new Scope
                        {
                            Name = adminConfiguration.IdentityAdminApiScope,
                            DisplayName = adminConfiguration.IdentityAdminApiScope,
                            UserClaims = new List<string>
                            {
                                "role"
                            },
                            Required = true
                        }
                    }
                }*/
            };
        }
    }
}