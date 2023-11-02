using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Web2Identity.API;

namespace Web2.API.Extenstions
{
    public static class ServiceCollectionExtenstions
    {
        public static void RegisterAuthentication(this IServiceCollection services) 
        {
            services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.Authority = "https://localhost:5001";
                options.Audience = "Web2Api";
                options.TokenValidationParameters.ValidTypes = new[] { "at+jwt" };

                options.TokenValidationParameters.ValidateAudience = true;
            });

            services.AddAuthorization(pb =>
            {
                pb.AddPolicy("requireApiScope", o => {
                    o.RequireAuthenticatedUser().RequireClaim("scope", "web2ApiScope");
                });
                pb.AddPolicy("requireAdmin",
                    o => o.RequireRole("admin")
                    .Combine(pb.DefaultPolicy));
                pb.AddPolicy("requireManager", o => o.RequireRole("manager").Combine(pb.DefaultPolicy));

                pb.DefaultPolicy = pb.GetPolicy("requireApiScope");
            });
        }

        public static void RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web2.API", Version = "v1" });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri("https://localhost:5001/connect/authorize"),
                            TokenUrl = new Uri("https://localhost:5001/connect/token"),
                            Scopes = new Dictionary<string, string>
                                {
                                    {"web2ApiScope", "Demo API - default scope"},
                                    {"scope2", "Demo API - scope 2"}
                                }
                        }
                    }
                });
                c.OperationFilter<AuthorizeCheckOperationFilter>();
            });
        }
    }
}
