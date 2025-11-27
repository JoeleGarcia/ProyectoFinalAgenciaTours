using ProyectoFinalAgenciaTours.Application.DTOs;
using System;
using System.Collections.Generic;


namespace ProyectoFinalAgenciaTours.Application.Interfaces
{
    public interface ILoginService
    {

        Task<bool> RegisterAsync(RegistroUsuarioDto registroUsuarioDto);
        Task<UsuarioDto> LoginAsync(string email, string password);
        Task<LoginJwtResponseDto> LoginForJwtAsync(LoginUsuarioDto loginUsuarioDto);
        Task LogoutAsync();
        Task<string> HashPassword(string password);
        string GetUserId();
        Task<UsuarioDto> UserInfo(string id);

    }
}
