using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalAgenciaTours.Infrastructure.Data;
using ProyectoFinalAgenciaTours.Application.Exceptions;
using ProyectoFinalAgenciaTours.Domain.Entities;
using ProyectoFinalAgenciaTours.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAgenciaTours.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext _dbcontext;

        public UserRepository(ApplicationDbContext context)
        {
            _dbcontext = context;
        }

        public Task<bool> AddUserAsync(Usuario user)
        {
            try
            {
                _dbcontext.Usuarios.Add(user);
                var result = _dbcontext.SaveChangesAsync();
                return Task.FromResult(result.Result > 0);
            }
            catch (SqlException ex) when (ex.Number == -2) // Timeout específico
            {
                throw new InfrastructureException("Timeout al consultar la base de datos", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new InfrastructureException("Error en la consulta de usuario", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new InfrastructureException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new InfrastructureException("Error inesperado en la base de datos", ex);
            }
        }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            return await _dbcontext.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Usuario> GetUserByIdAsync(Guid id)
        {
            try
            {
                var user = await _dbcontext.Usuarios.FirstOrDefaultAsync(u => u.Id.Equals(id));

                if (user == null)
                    return null!;

                return user;
            }
            catch (SqlException ex) when (ex.Number == -2) // Timeout específico
            {
                throw new InfrastructureException("Timeout al consultar la base de datos", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new InfrastructureException("Error en la consulta de usuario", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new InfrastructureException("Error en la base de datos", ex);
            }
            catch (Exception ex)
            {
                throw new InfrastructureException("Error inesperado en la base de datos", ex);
            }

        }

        public Task<Usuario?> GetByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsEmailRegisteredAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsUsernameRegisteredAsync(string username)
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> ValidateCredentialsAsync(string email, string password)
        {
            try
            {
                var user = await _dbcontext.Usuarios.FirstOrDefaultAsync(u => u.Email == email && u.Password.Equals(password));

                if (user == null)
                    return null!; // usuario no existe

                return user; // login correcto
            }
            catch (SqlException ex) when (ex.Number == -2) // Timeout específico
            {
                throw new InfrastructureException("Timeout al consultar la base de datos", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new InfrastructureException("Error en la consulta de usuario", ex);
            }
            catch (DbUpdateException ex)
            {
                throw new InfrastructureException("Error en la base de datos", ex);
            }
            catch (Exception ex)
            {
                throw new InfrastructureException("Error inesperado en la base de datos", ex);
            }

        }

    }
}
