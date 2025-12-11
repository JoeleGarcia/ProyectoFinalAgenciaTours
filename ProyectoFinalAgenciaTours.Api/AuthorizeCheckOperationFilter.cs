using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace ProyectoFinalAgenciaTours.Api
{
    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // 1. Verificar si el MÉTODO de la acción tiene [AllowAnonymous]
            // (La presencia de [AllowAnonymous] en el método tiene la máxima prioridad)
            var methodAllowAnonymous = context.MethodInfo.GetCustomAttributes(true)
                .OfType<AllowAnonymousAttribute>().Any();

            // 2. Verificar si el CONTROLADOR tiene [Authorize]
            var controllerAuthorize = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                .OfType<AuthorizeAttribute>().Any();

            // 3. Verificar si el MÉTODO de la acción tiene [Authorize]
            var methodAuthorize = context.MethodInfo.GetCustomAttributes(true)
                .OfType<AuthorizeAttribute>().Any();

            // 4. Determinar si se requiere autorización
            // Se requiere autorización si el controlador TIENE [Authorize] O el método TIENE [Authorize]
            // Y el MÉTODO NO TIENE [AllowAnonymous]
            bool requiresAuthorization = (controllerAuthorize || methodAuthorize) && !methodAllowAnonymous;

            // 5. Si la autorización NO es requerida, salimos y no añadimos el candado.
            if (!requiresAuthorization)
            {
                // Opcional: Esto asegura que si se aplicó seguridad globalmente, se elimina para este endpoint.
                operation.Security = new List<OpenApiSecurityRequirement>();
                return;
            }

            // Si se requiere autorización (candado)

            operation.Security ??= new List<OpenApiSecurityRequirement>();

            // 6. Añadir el requisito "Bearer" para habilitar el candado.
            operation.Security.Add(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer" // Debe coincidir con el ID definido en AddSecurityDefinition
                    }
                },
                new string[] { }
            }
        });

            // Añadir documentación de la respuesta 401
            operation.Responses.TryAdd("401", new OpenApiResponse { Description = "Unauthorized" });
        }
    }
}
