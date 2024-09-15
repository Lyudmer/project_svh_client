using Amazon.Auth.AccessControlPolicy;
using ClientSVH.Core.Models;
using ClientSVH.Endpoints;
using ClientSVH.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace ClientSVH.Extensions
{
    public static class ApiExtentions
    {
        public static void AddMappedEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapUsersEndpoints();
        }
        public static void AddApiAuthentication(this IServiceCollection services, 
            IConfiguration configuration)
        {
           var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
            {
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
