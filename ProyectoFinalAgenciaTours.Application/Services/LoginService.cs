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
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAgenciaTours.Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository _userRepository;
        private readonly ISessionManager _sessionManagerRepository;
        private readonly ITokenService _tokenRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public LoginService(
                IUserRepository userRepository
            ,   ISessionManager sessionManagerRepository
            ,   IHttpContextAccessor httpContextAccessor
            ,   ITokenService tokenRepository
            ,   IMapper mapper
        )
        {
            _userRepository = userRepository;
            _sessionManagerRepository = sessionManagerRepository;
            _tokenRepository = tokenRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public string GetUserId()
        {
            string userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)!;
            return userId;
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

        public async Task<LoginJwtResponseDto> LoginForJwtAsync(LoginUsuarioDto loginUsuarioDto)
        {
            var userDetails = await _userRepository.ValidateCredentialsAsync(loginUsuarioDto.Email, loginUsuarioDto.Password);

            if (userDetails is null)
            {
                return null!;
            }

            var _user = _mapper.Map<UsuarioDto>(userDetails);

            //throw new NotImplementedException();
            return await _tokenRepository.GenerarJwt(_user);
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

        public async Task<UsuarioDto> UserInfo(string id)
        {
            if (!Guid.TryParse(id, out Guid _id))
            {
                throw new ArgumentException("El formato del ID proporcionado no es válido.");
            }
            var user = await _userRepository.GetUserByIdAsync(_id);

            var _user = _mapper.Map<UsuarioDto>(user);

            return _user;
            //return new UsuarioDto(user.Id, user.Nombre, user.Apellido, user.Username, user.Email, user.Role, user.Status);
        }
    }
}
