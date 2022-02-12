// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace Paulo.IDP
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiResource> ApiResources => new[]
        {
            new ApiResource("mysocialnetworkapi")
            {
                Scopes= new List<string> { "mysocialnetworkapi.read", "mysocialnetworkapi.write" },
                ApiSecrets = new List<Secret> { new Secret("ApiSecret".Sha256())},
                UserClaims = new List<string> {"given_name"}
            }

        };
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("mysocialnetworkapi.read"),
                new ApiScope("mysocialnetworkapi.write")
            };

        public static IEnumerable<Client> Clients =>
            new Client[] 
            {
                new Client
                {
                    ClientName="My Social Network",
                    ClientId="mysocialnetworkclient",
                    AllowedGrantTypes = GrantTypes.ImplicitAndClientCredentials,
                    AllowAccessTokensViaBrowser =true,
                    RedirectUris = new List<string>()
                    {
                        "https://localhost:4200/signin-oidc",
                        "https://oauth.pstmn.io/v1/browser-callback"
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                         "https://localhost:4200/"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "mysocialnetworkapi.read",
                        "mysocialnetworkapi.write"
                    },
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    }
                }
            };
    }
}