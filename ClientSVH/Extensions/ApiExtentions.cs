using Amazon.Auth.AccessControlPolicy;
using ClientSVH.Core.Models;
using ClientSVH.Endpoints;
using ClientSVH.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;


namespace ClientSVH.Extensions
{
    public static class ApiExtentions
    {
        public static void AddMappedEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapUsersEndpoints();
            app.MapPackagesEndpoints();
            app.MapStatusEndpoints();
            
        }
        public static void AddApiAuthentication(this IServiceCollection services, 
            IConfiguration configuration)
        {
           var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

            services.AddAuthentication(Options =>
            {
                Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken=true;
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions!.SecretKey))

                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context => 
                    {
                        context.Token = context.Request.Cookies["tu-cookes"];
                        return Task.CompletedTask;
                    }
                };
            }
            );
            services.AddAuthorization();
        }
    }
}
