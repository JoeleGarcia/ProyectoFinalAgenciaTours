using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using ProyectoFinalAgenciaTours.Application.DTOs;
using ProyectoFinalAgenciaTours.Application.Interfaces;
using ProyectoFinalAgenciaTours.Application.Services;
using ProyectoFinalAgenciaTours.Application.Validator;
using SistemaCalificacion.Application.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAgenciaTours.Application
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IPaisService, PaisService>();
            services.AddScoped<IDestinoService, DestinoService>();
            services.AddScoped<ITourService, TourService>();

            services.AddScoped<IValidator<LoginUsuarioDto>, LoginUserDtoValidator>();
            services.AddScoped<IValidator<RegistroUsuarioDto>, RegisterUserDtoValidator>();
            services.AddScoped<IValidator<UsuarioDto>, UsuarioDtoValidator>();

            services.AddScoped<IValidator<CreateDestinoDto>, CreateDestinoDtoValidator>();
            services.AddScoped<IValidator<UpdateDestinoDto>, UpdateDestinoDtoValidator>();

            services.AddScoped<IValidator<CreatePaisDto>, CreatePaisDtoValidator>();
            services.AddScoped<IValidator<UpdatePaisDto>, UpdatePaisDtoValidator>();

            services.AddScoped<IValidator<CreateTourDto>, CreateTourDtoValidator>();
            services.AddScoped<IValidator<UpdateTourDto>, UpdateTourDtoValidator>();
            services.AddFluentValidationAutoValidation();

            services.AddAutoMapper(typeof(DependencyInjection).Assembly);
            return services;
        }

    }
}
