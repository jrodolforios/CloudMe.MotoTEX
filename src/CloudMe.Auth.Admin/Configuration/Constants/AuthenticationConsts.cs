using System.Collections.Generic;

namespace CloudMe.Auth.Admin.Configuration.Constants
{
    public class AuthenticationConsts
    {
        public const string IdentityAdminCookieName = "IdentityServerAdmin";        
        public const string UserNameClaimType = "name";
        public const string SignInScheme = "Cookies";
        public const string OidcClientId = "cf8ad8df-5cf6-4963-8768-6534e02220a8";
        public const string OidcClientName = "CloudMe Auth Admin";
        public const string OidcClientSecret = "79c40099-ab83-44b7-9bcd-676b35ad1601";
        public const string OidcAuthenticationScheme = "oidc";
        public const string OidcResponseType = "id_token";
        public static List<string> Scopes = new List<string> { "openid", "profile", "email", "roles" };
        
        public const string ScopeOpenId = "openid";
        public const string ScopeProfile = "profile";
        public const string ScopeEmail = "email";
        public const string ScopeRoles = "roles";

        public const string AccountLoginPage = "Account/Login";
        public const string AccountAccessDeniedPage = "/Account/AccessDenied/";
    }
}