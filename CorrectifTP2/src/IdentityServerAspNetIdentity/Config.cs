// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServerAspNetIdentity
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("web2ApiScope"),
                new ApiScope("scope2"),
            };

        public static IEnumerable<ApiResource> ApiRessources =>
          new ApiResource[]
          {
                //new ApiResource("Web2Api", "API #1", new[] { ClaimTypes.Role}) {Scopes = {"web2ApiScope"}}
                new ApiResource("Web2Api", "API #1", new[] { JwtClaimTypes.Role}) {Scopes = {"web2ApiScope"}}
          };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "swagger_ui",
                    ClientName = "Swagger UI for dev",
                    ClientSecrets = {new Secret("secret".Sha256())}, // change me!

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = false,
                    RequireClientSecret = false,

                    RedirectUris = {"https://localhost:7284/swagger/oauth2-redirect.html"},
                    AllowedCorsOrigins = {"https://localhost:7284"},
                    AllowedScopes = { "web2ApiScope", "scope2"}
                },

                new Client
                {
                    ClientId = "web2_ui",
                    ClientName = "Web2.UI Vuejs oidc client",
                    ClientSecrets = { new Secret("secretTP3jdnj".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RedirectUris = {
                        "http://localhost:8080/auth/signinsilent/vuejs",
                        "http://localhost:8080/auth/signinwin/vuejs",
                        "http://localhost:8080/auth/signinpop/vuejs"
                    },
                    PostLogoutRedirectUris = {"http://localhost:8080/" },
                    AllowedCorsOrigins = {"http://localhost:8080"},
                    AllowedScopes = {
                        "web2ApiScope",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                    }
                },

            };

    }
}