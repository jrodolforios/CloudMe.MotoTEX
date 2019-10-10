using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;
using CloudMe.Auth.Admin.Configuration.Constants;
using CloudMe.Auth.Admin.Configuration.Interfaces;

namespace CloudMe.Auth.Admin.Configuration.IdentityServer

{
    public class Clients
    {

		public static IEnumerable<Client> GetAdminClient(IAdminConfiguration adminConfiguration)
		{
			return new List<Client>
			{
	            new Client
                {
                    ClientId = AuthenticationConsts.OidcClientId,
                    ClientName = AuthenticationConsts.OidcClientName,
                    ClientUri = adminConfiguration.IdentityAdminBaseUrl,

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { $"{adminConfiguration.IdentityAdminBaseUrl}/signin-oidc"},
                    FrontChannelLogoutUri = $"{adminConfiguration.IdentityAdminBaseUrl}/signout-oidc",
                    PostLogoutRedirectUris = { $"{adminConfiguration.IdentityAdminBaseUrl}/signout-callback-oidc"},
                    AllowedCorsOrigins = { adminConfiguration.IdentityAdminBaseUrl },
                    ClientSecrets ={new Secret(AuthenticationConsts.OidcClientSecret.Sha256())},
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "roles"
                    }
                },
				// resource owner password grant client
				new Client
				{
					ClientId = "ToDeTaxiAPI_admin",
					ClientName = "Portal admnistrativo do sistema TOdeTaxi",
					AllowedGrantTypes = GrantTypes.Implicit,
					AllowedScopes = { "todetaxiapi" },
					AllowAccessTokensViaBrowser = true,
					RequireConsent = true,
					/*ClientSecrets =
					{
						new Secret("secret".Sha256())
					},*/
					RedirectUris = {
						"http://localhost:4200/auth/callback",
                        "http://admin.todetaxi.com.br/auth/callback",
                        "http://passenger.todetaxi.com.br/auth/callback",
                        "http://driver.todetaxi.com.br/auth/callback",

                        "https://localhost:4200/auth/callback",
                        "https://admin.todetaxi.com.br/auth/callback",
                        "https://passenger.todetaxi.com.br/auth/callback",
                        "https://driver.todetaxi.com.br/auth/callback"
                    },
					PostLogoutRedirectUris = {
						"http://localhost:4200",
						"https://admin.todetaxi.com.br",
                        "http://passenger.todetaxi.com.br",
                        "http://driver.todetaxi.com.br",
                        "https://passenger.todetaxi.com.br",
                        "https://driver.todetaxi.com.br"
                    }
				},
				new Client
				{
					ClientId = "ToDeTaxiAPI_mobile",
					ClientName = "Aplicativo mobile do sistema TOdeTaxi",
					AllowedScopes = {"todetaxiapi"},
					AllowedGrantTypes = GrantTypes.Implicit,
					AllowAccessTokensViaBrowser = true,
					RedirectUris = {
                        "http://localhost:8100/auth/callback",
                        "http://passenger.todetaxi.com.br/auth/callback",
                        "http://driver.todetaxi.com.br/auth/callback",

                        "https://localhost:8100/auth/callback",
                        "https://passenger.todetaxi.com.br/auth/callback",
                        "https://driver.todetaxi.com.br/auth/callback"
                    },
                    PostLogoutRedirectUris = {
                        "http://localhost:8100",
                        "http://passenger.todetaxi.com.br",
                        "http://driver.todetaxi.com.br",

                        "https://localhost:8100",
                        "https://passenger.todetaxi.com.br",
                        "https://driver.todetaxi.com.br"
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
						"http://localhost:5002/swagger/oauth2-redirect.html",
						"http://localhost:5002/swagger/o2c.html",
						"http://localhost:5002/swagger/signin-oidc",
						"http://localhost:5002/oauth2-redirect.html",
						"http://localhost:5002/o2c.html",
						"http://localhost:5002/signin-oidc",

                        "https://api.todetaxi.com.br/swagger/oauth2-redirect.html",
                        "https://api.todetaxi.com.br/swagger/o2c.html",
                        "https://api.todetaxi.com.br/swagger/signin-oidc",
                        "https://api.todetaxi.com.br/oauth2-redirect.html",
                        "https://api.todetaxi.com.br/o2c.html",
                        "https://api.todetaxi.com.br/signin-oidc"
                    },
				}
			};
		}
    }
}
