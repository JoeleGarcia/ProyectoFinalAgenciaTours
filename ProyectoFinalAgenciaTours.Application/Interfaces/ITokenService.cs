using ProyectoFinalAgenciaTours.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAgenciaTours.Application.Interfaces
{
    public interface ITokenService
    {

        Task<LoginJwtResponseDto> GenerarJwt(UsuarioDto usuarioDto);

    }
}
