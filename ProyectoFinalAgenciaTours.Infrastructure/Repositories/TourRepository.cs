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
    public class TourRepository : ITourRepository
    {

        private readonly ApplicationDbContext _dbcontext;

        public TourRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task<Tour> AddTourAsync(Tour tour)
        {
            try
            {
                await _dbcontext.Tours.AddAsync(tour);
                await _dbcontext.SaveChangesAsync();
                return tour;
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

        public async Task DeleteTourAsync(int id)
        {
            var _tour = await GetTourByIdAsync(id);

            if (_tour is null)
                throw new NotFoundException("Tour", string.Format("By Id {id}", id.ToString()));

            _dbcontext.Tours.Remove(_tour);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Tour>> GetAllTouresAsync()
        {
            return await _dbcontext.Tours
                            .Include(e => e.Pais)
                            .Include(m => m.Destino)
                            .ToListAsync();
        }

        public async Task<Tour?> GetTourByIdAsync(int id)
        {
            return await _dbcontext.Tours.AsNoTracking()
                            .Include(e => e.Pais) 
                            .Include(m => m.Destino)
                            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateTourAsync(Tour tour)
        {
            _dbcontext.Tours.Update(tour);
            await _dbcontext.SaveChangesAsync();
        }
    }
}
