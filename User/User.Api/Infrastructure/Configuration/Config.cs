using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Api.Infrastructure.Configuration
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
            };
        }
        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("booking.api", "Booking Api"),
                new ApiResource("identity.api", "Identity Api"),
                new ApiResource("user.api", "User Api"),
                new ApiResource("restaurant.api", "Restaurant Api"),
            };
        }
        public static IEnumerable<ApiScope> GetScopes()
        {
            return new List<ApiScope> {
                new ApiScope("user.api.user"),
                new ApiScope("user.api.admin"),
                new ApiScope("restaurant.api.admin"),
                new ApiScope("restaurant.api.user"),
                new ApiScope("booking.api.user"),
                new ApiScope("booking.api.admin")
            };
        }
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                 new Client
                {
                    ClientId = "angular.client",
                    ClientName = "Angular Client",
                    ClientSecrets = {new Secret("67822fbd-110d-4ab3-b55f-e7441cde52fa".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowAccessTokensViaBrowser = true,
                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile" ,"user.api.user","restaurant.api.user","booking.api.user",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address, },


                },
                 new Client
                 {
                     ClientId ="angular.admin",
                     ClientName = "Angular Admin",
                     ClientSecrets = {new Secret("b7bd6cc6-c01b-42e7-b9fa-550120617506".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowAccessTokensViaBrowser = true,
                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile" ,"user.api.admin","restaurant.api.admin","booking.api.admin",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Address, },
                    Claims = new List<ClientClaim>
                    {
                        new ClientClaim("role","admin")
                    }
                 }
            };
        }

    }
}
