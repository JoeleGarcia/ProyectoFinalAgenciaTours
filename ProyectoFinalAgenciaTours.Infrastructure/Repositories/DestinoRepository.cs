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
    public class DestinoRepository : IDestinoRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public DestinoRepository(ApplicationDbContext context)
        {
            _dbcontext = context;
        }
        public async Task<Destino> AddDestinosAsync(Destino destinos)
        {
            try
            {
                await _dbcontext.Destinos.AddAsync(destinos);
                await _dbcontext.SaveChangesAsync();
                return destinos;
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

        public async Task DeleteDestinosAsync(int id)
        {
            var _destino = await GetDestinosByIdAsync(id);

            if (_destino is null)
                throw new NotFoundException("Destino", string.Format("By Id {id}", id.ToString()));

            _dbcontext.Destinos.Remove(_destino);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Destino>> GetAllDestinosAsync()
        {
            return await _dbcontext.Destinos
            .Include(e => e.Pais)
            .ToListAsync();
        }

        public async Task<Destino?> GetDestinosByIdAsync(int id)
        {
            return await _dbcontext.Destinos.AsNoTracking()
                                .FirstOrDefaultAsync(p => p.Id == id); ;
        }

        public async Task UpdateDestinosAsync(Destino destinos)
        {
            _dbcontext.Destinos.Update(destinos);
            await _dbcontext.SaveChangesAsync();
        }
    }
}
