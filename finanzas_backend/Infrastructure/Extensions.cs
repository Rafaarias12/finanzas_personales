using finanzas_backend.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace finanzas_backend.Infrastructure
{
    public static class Extensions
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwtSettings:SecretKey"]!));
            services.AddSingleton(key);
            
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddHttpContextAccessor();
            
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Finanzas Personales API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    option.IncludeXmlComments(xmlPath);
                }

                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("develop"));
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = configuration["jwtSettings:Issuer"],
                     ValidAudience = configuration["jwtSettings:Audience"],
                     IssuerSigningKey = key,
                 };
             });

            services.AddAuthorization();
        }

        public static string GetClaimValue(this ClaimsPrincipal userClaimsPrincipal, string claimType)
        {
            return (from claim in userClaimsPrincipal.Claims
                    where claim.Type.Equals(claimType, StringComparison.InvariantCultureIgnoreCase)
                    select claim.Value).FirstOrDefault()!;
        }
    }
}
