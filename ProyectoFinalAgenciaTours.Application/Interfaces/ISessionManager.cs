using ProyectoFinalAgenciaTours.Application.DTOs;
using ProyectoFinalAgenciaTours.Domain.Entities;

namespace ProyectoFinalAgenciaTours.Application.Interfaces
{
    public interface ISessionManager
    {
        Task SignInAsync(UsuarioDto user);
        Task SignOutAsync();

    }
}
