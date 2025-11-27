using ProyectoFinalAgenciaTours.Application.DTOs;
using ProyectoFinalAgenciaTours.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAgenciaTours.Application.Services
{
    public class LoginService : ILoginService
    {
        public string GetUserId()
        {
            throw new NotImplementedException();
        }

        public Task<string> HashPassword(string password)
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioDto> LoginAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<LoginJwtResponseDto> LoginForJwtAsync(LoginUsuarioDto loginUsuarioDto)
        {
            throw new NotImplementedException();
        }

        public Task LogoutAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegisterAsync(RegistroUsuarioDto registroUsuarioDto)
        {
            throw new NotImplementedException();
        }

        public Task<UsuarioDto> UserInfo(string id)
        {
            throw new NotImplementedException();
        }
    }
}
