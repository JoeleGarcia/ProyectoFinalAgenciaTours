using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProyectoFinalAgenciaTours.Application.DTOs;
using ProyectoFinalAgenciaTours.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAgenciaTours.Infrastructure.Repositories.Security
{
    public class TokenRepository : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly byte[] _keyBytes;

        public TokenRepository(IConfiguration config)
        {
            _config = config;

            string secretKey = _config["Jwt:Key"]
                ?? throw new InvalidOperationException("Jwt:Key no encontrado en la configuración.");

            _keyBytes = Encoding.ASCII.GetBytes(secretKey);
        }

        public Task<LoginJwtResponseDto> GenerarJwt(UsuarioDto usuarioDto)
        {

            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuarioDto.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, usuarioDto.Username),
                new Claim(ClaimTypes.Role, usuarioDto.Role),
            };

            var securityKey = new SymmetricSecurityKey(_keyBytes);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            if (!double.TryParse(_config["Jwt:LifetimeMinutes"], out double lifetimeMinutes))
            {
                lifetimeMinutes = 60;
            }

            var expires = DateTime.UtcNow.AddMinutes(lifetimeMinutes);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject             = new ClaimsIdentity(claims),
                Issuer              = _config["Jwt:Issuer"],
                Audience            = _config["Jwt:Audience"],
                Expires             = expires,
                SigningCredentials  = credentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string accessToken = tokenHandler.WriteToken(token);

            var responseDto = new LoginJwtResponseDto(
                AccessToken: accessToken,
                UserName: usuarioDto.Username,
                ExpiresIn: (int)Math.Round((expires - DateTime.UtcNow).TotalSeconds)
            );

            return Task.FromResult(responseDto);

        }
    }
}
