using AutoMapper;
using Microsoft.AspNetCore.Http;
using ProyectoFinalAgenciaTours.Application.DTOs;
using ProyectoFinalAgenciaTours.Application.Exceptions;
using ProyectoFinalAgenciaTours.Application.Interfaces;
using ProyectoFinalAgenciaTours.Domain.Entities;
using ProyectoFinalAgenciaTours.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAgenciaTours.Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISessionManager _sessionManagerRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public LoginService(
                IUserRepository userRepository
            ,   ISessionManager sessionManagerRepository
            ,   IHttpContextAccessor httpContextAccessor
            ,   IMapper mapper
        )
        {
            _userRepository = userRepository;
            _sessionManagerRepository = sessionManagerRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public string GetUserId()
        {
            throw new NotImplementedException();
        }

        public Task<string> HashPassword(string password)
        {
            throw new NotImplementedException();
        }

        public async Task<UsuarioDto> LoginAsync(string username, string password)
        {
            var response = await _userRepository.ValidateCredentialsAsync(username, password);

            if (response is null)
            {
                return null!;
            }

            await _sessionManagerRepository.SignInAsync(response);

            return new UsuarioDto(response.Id, response.Nombre, response.Apellido, response.Username, response.Email, response.Role, response.Status);
        }

        public Task<LoginJwtResponseDto> LoginForJwtAsync(LoginUsuarioDto loginUsuarioDto)
        {
            throw new NotImplementedException();
        }

        public async Task LogoutAsync()
        {
            await _sessionManagerRepository.SignOutAsync();
        }

        public async Task<bool> RegisterAsync(RegistroUsuarioDto registroUsuarioDto)
        {
            try
            {
                var user = new Usuario
                {
                    Username = registroUsuarioDto.Username,
                    Password = registroUsuarioDto.Password,
                    Email = registroUsuarioDto.Email,
                    Nombre = registroUsuarioDto.Nombre,
                    Apellido = registroUsuarioDto.Apellido
                };

                var userRegistered = await _userRepository.AddUserAsync(user);

                return userRegistered;
            }
            catch (InfrastructureException ex)
            {
                throw new ApplicationException("No se pudo registrar el usuario. Intente más tarde.", ex);
            }
        }

        public Task<UsuarioDto> UserInfo(string id)
        {
            throw new NotImplementedException();
        }
    }
}
