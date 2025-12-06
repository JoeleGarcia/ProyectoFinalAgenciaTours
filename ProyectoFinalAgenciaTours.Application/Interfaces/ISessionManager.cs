using ProyectoFinalAgenciaTours.Application.DTOs;
using ProyectoFinalAgenciaTours.Domain.Entities;

namespace ProyectoFinalAgenciaTours.Application.Interfaces
{
    public interface ISessionManager
    {
        Task SignInAsync(Usuario user);
        Task SignOutAsync();

    }
}
