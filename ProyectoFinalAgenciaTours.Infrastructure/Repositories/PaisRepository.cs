using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalAgenciaTours.Application.Exceptions;
using ProyectoFinalAgenciaTours.Domain.Entities;
using ProyectoFinalAgenciaTours.Domain.Interfaces;
using ProyectoFinalAgenciaTours.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAgenciaTours.Infrastructure.Repositories
{
    public class PaisRepository : IPaisRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public PaisRepository(ApplicationDbContext context)
        {
            _dbcontext = context;
        }
        public async Task<Pais> AddPaisAsync(Pais pais)
        {
            try
            {
                await _dbcontext.Paises.AddAsync(pais);
                await _dbcontext.SaveChangesAsync();
                return pais;
            }
            catch (SqlException ex) when (ex.Number == -2)
            {
                throw new InfrastructureException("Timeout al consultar la base de datos", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new InfrastructureException("Error en la consulta de Materia", ex);
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

        public async Task DeletePaisAsync(int id)
        {
            var _pais = await GetPaisByIdAsync(id);

            if (_pais is null)
                throw new NotFoundException("Pais", string.Format("By Id {id}", id.ToString()));

            _dbcontext.Paises.Remove(_pais);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Pais>> GetAllPaisAsync()
        {
            return await _dbcontext.Paises.ToListAsync();

        }

        public async Task<Pais?> GetPaisByIdAsync(int id)
        {
            return await _dbcontext.Paises.AsNoTracking()
                                            .FirstOrDefaultAsync(p => p.Id == id); ;
        }

        public async Task UpdatePaisAsync(Pais pais)
        {
            _dbcontext.Paises.Update(pais);
            await _dbcontext.SaveChangesAsync();
        }
    }
}
