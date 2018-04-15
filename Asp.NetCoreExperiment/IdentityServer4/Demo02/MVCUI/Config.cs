using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVCUI
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };
        }
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
    // OpenID Connect implicit flow client (MVC)
        new Client
        {
            ClientId = "mvc",
            ClientName = "MVC Client",
            AllowedGrantTypes = GrantTypes.Implicit,

            // where to redirect to after login
            RedirectUris = { "http://localhost:5002/signin-oidc" },

            // where to redirect to after logout
            PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

            AllowedScopes = new List<string>
            {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile
            }
        }
        };
        }
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
    {
        new TestUser
        {
            SubjectId = "1",
            Username = "gsw",
            Password = "111111",

            Claims = new []
            {
                new Claim("name", "gsw"),
                new Claim("website", "https://alice.com")
            }
        },
        new TestUser
        {
            SubjectId = "2",
            Username = "ggg",
            Password = "222222",

            Claims = new []
            {
                new Claim("name", "Bob"),
                new Claim("website", "https://bob.com")
            }
        }
    };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),    };

        }
    }
}
