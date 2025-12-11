using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ProyectoFinalAgenciaTours.Application.Interfaces;
using ProyectoFinalAgenciaTours.Application.Services;
using ProyectoFinalAgenciaTours.Domain.Interfaces;
using ProyectoFinalAgenciaTours.Infrastructure.Data;
using ProyectoFinalAgenciaTours.Infrastructure.Repositories;
using ProyectoFinalAgenciaTours.Infrastructure.Repositories.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAgenciaTours.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(option =>
            option.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISessionManager, SessionManagerRepository>();
            services.AddScoped<IPaisRepository, PaisRepository>();
            services.AddScoped<IDestinoRepository, DestinoRepository>();
            services.AddScoped<ITourRepository, TourRepository>();

            services.AddScoped<ITokenService, TokenRepository>();


            services.AddAuthentication().AddCookie("MyCookieAuth", options =>
            {
                options.Cookie.Name = "MyCookieAuth";
                options.LoginPath = "/Account/Login";
            });

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //}).AddJwtBearer(options =>
            //{
            //    options.RequireHttpsMetadata = false;
            //    options.SaveToken = true;
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)),

            //        ValidateIssuer = true,
            //        ValidIssuer = configuration["Jwt:Issuer"],

            //        ValidateAudience = true,
            //        ValidAudience = configuration["Jwt:Audience"],

            //        ValidateLifetime = true,
            //        ClockSkew = TimeSpan.Zero
            //    };
            //});

            return services;
        }
    }
}
