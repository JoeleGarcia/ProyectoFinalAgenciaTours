using ProyectoFinalAgenciaTours.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAgenciaTours.Domain.Interfaces
{
    public interface ITourRepository
    {
        Task<IEnumerable<Tour>> GetAllTouresAsync();
        Task<Tour?> GetTourByIdAsync(int id);
        Task<Tour> AddTourAsync(Tour tour);
        Task UpdateTourAsync(Tour tour);
        Task DeleteTourAsync(int id);
    }
}
