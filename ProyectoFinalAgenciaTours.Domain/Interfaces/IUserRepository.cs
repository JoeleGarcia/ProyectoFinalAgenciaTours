using ProyectoFinalAgenciaTours.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAgenciaTours.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> AddUserAsync(Usuario user);
        Task<Usuario> GetUserByIdAsync(Guid id);
        Task<Usuario?> GetByEmailAsync(string email);
        Task<Usuario?> GetByUsernameAsync(string username);
        Task<bool> IsEmailRegisteredAsync(string email);
        Task<bool> IsUsernameRegisteredAsync(string username);
        Task<Usuario> ValidateCredentialsAsync(string email, string password);
    }
}
