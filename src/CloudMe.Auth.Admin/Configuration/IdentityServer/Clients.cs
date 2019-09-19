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
				// resource owner password grant client
				new Client
				{
					ClientId = "ToDeTaxiAPI_admin",
					ClientName = "Portal admnistrativo do sistema TOdeTaxi",
					AllowedGrantTypes = new List<string> { "authorization_code" },
					AllowedScopes = { "todetaxiapi" },
					AllowAccessTokensViaBrowser = true,
					RequireConsent = true,
					RedirectUris = {
						"http://localhost:4200/login-callback",
					},
					PostLogoutRedirectUris = {
						"http://localhost:4200"
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
						"http://localhost:4200/login-callback",
					},
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
						"http://localhost:5002/signin-oidc"
					},
				}
			};
		}
    }
}
