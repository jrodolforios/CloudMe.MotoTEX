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
                new Client
                {
                    ClientId = "MotoTEXAPI_admin",
                    ClientName = "Portal admnistrativo do sistema MotoTEX",
                    Enabled = true,
                    //AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedGrantTypes = new List<string> { "authorization_code" },
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "roles",
                        "nome",
                        "mototexapi"
                    },
                    AlwaysSendClientClaims = true,
                    AccessTokenType = AccessTokenType.Jwt,
                    RequireConsent = false,
                    AllowRememberConsent = false,
                    AllowAccessTokensViaBrowser = true,
                    //AuthorizationCodeLifetime = 60,
                    RefreshTokenUsage = TokenUsage.OneTimeOnly,
                    RefreshTokenExpiration = TokenExpiration.Absolute,
                    AllowOfflineAccess = true,
                    //RequirePkce = true,
                    ClientSecrets =
					{
						new Secret("4b80ab4c-7600-42de-991b-bb402bbf206d".Sha256())
					},
					RedirectUris = {
						"http://localhost:4200/auth/callback",
                        "http://localhost:4200/auth/silent-refresh.html",
                        "http://admin.MotoTEX.com.br/auth/callback",
                        "http://admin.MotoTEX.com.br/auth/silent-refresh.html",

                        "https://localhost:4200/auth/callback",
                        "https://localhost:4200/auth/silent-refresh.html",
                        "https://admin.MotoTEX.com.br/auth/callback",
                        "https://admin.MotoTEX.com.br/auth/silent-refresh.html",
                    },
					PostLogoutRedirectUris = {
						"http://localhost:4200",
						"https://admin.MotoTEX.com.br",
                    }
				},
				new Client
				{
					ClientId = "MotoTEXAPI_mobile",
					ClientName = "Aplicativo mobile do sistema MotoTEX",
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "mototexapi",
                        "nome"
                    },
                    AlwaysSendClientClaims = true,
                    RequireConsent = false,
                    AllowRememberConsent = false,
                    AllowedGrantTypes = new List<string> { "authorization_code" },
                    AllowOfflineAccess = true,
                    RequirePkce = true,
                    AllowAccessTokensViaBrowser = true,
                    ClientSecrets =
                    {
                        new Secret("9740e17e-9867-4477-8285-bb78485bdf2d".Sha256())
                    },
					RedirectUris = {
                        "http://localhost:8100/auth/callback",
                        "http://localhost:8100/#/callback/?",
                        "http://localhost:8100/auth/silent-refresh.html",
                        "http://passenger.MotoTEX.com.br/auth/callback",
                        "http://driver.MotoTEX.com.br/auth/callback",
                        "http://driver.MotoTEX.com.br/#/callback/?",
                        "http://passenger.MotoTEX.com.br/auth/silent-refresh.html",
                        "http://driver.MotoTEX.com.br/auth/silent-refresh.html",

                        "https://localhost:8100/auth/callback",
                        "https://localhost:8100/#/callback/?",
                        "https://localhost:8100/auth/silent-refresh.html",
                        "https://passenger.MotoTEX.com.br/auth/callback",
                        "https://driver.MotoTEX.com.br/auth/callback",
                        "https://driver.MotoTEX.com.br/#/callback/?",
                        "https://passenger.MotoTEX.com.br/auth/silent-refresh.html",
                        "https://driver.MotoTEX.com.br/auth/silent-refresh.html"
                    },
                    PostLogoutRedirectUris = {
                        "http://localhost:8100",
                        "http://passenger.MotoTEX.com.br",
                        "http://driver.MotoTEX.com.br",

                        "https://localhost:8100",
                        "https://passenger.MotoTEX.com.br",
                        "https://driver.MotoTEX.com.br"
                    }
                },
				new Client
				{
					ClientId = "MotoTEXAPI_swagger",
					ClientName = "API swagger do sistema MotoTEX",
					AllowedScopes = {"mototexapi"},
					AllowedGrantTypes = GrantTypes.Implicit,
                    RequireConsent = false,
                    AllowRememberConsent = false,
                    AllowAccessTokensViaBrowser = true,
					RedirectUris = {
						"http://localhost:5002/swagger/oauth2-redirect.html",
						"http://localhost:5002/swagger/o2c.html",
						"http://localhost:5002/swagger/signin-oidc",
						"http://localhost:5002/oauth2-redirect.html",
						"http://localhost:5002/o2c.html",
						"http://localhost:5002/signin-oidc",

                        "https://api.MotoTEX.com.br/swagger/oauth2-redirect.html",
                        "https://api.MotoTEX.com.br/swagger/o2c.html",
                        "https://api.MotoTEX.com.br/swagger/signin-oidc",
                        "https://api.MotoTEX.com.br/oauth2-redirect.html",
                        "https://api.MotoTEX.com.br/o2c.html",
                        "https://api.MotoTEX.com.br/signin-oidc"
                    },
				}
			};
		}
    }
}
