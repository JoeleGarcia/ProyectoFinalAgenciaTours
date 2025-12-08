using ProyectoFinalAgenciaTours.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAgenciaTours.Domain.Interfaces
{
    public interface IDestinoRepository
    {
        Task<IEnumerable<Destino>> GetAllDestinosAsync();
        Task<Destino?> GetDestinosByIdAsync(int id);
        Task<Destino> AddDestinosAsync(Destino destinos);
        Task UpdateDestinosAsync(Destino destinos);
        Task DeleteDestinosAsync(int id);
    }
}
