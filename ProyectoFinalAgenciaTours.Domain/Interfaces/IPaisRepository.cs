using ProyectoFinalAgenciaTours.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAgenciaTours.Domain.Interfaces
{
    public interface IPaisRepository
    {
        Task<IEnumerable<Pais>> GetAllPaisAsync();
        Task<Pais?> GetPaisByIdAsync(int id);
        Task<Pais> AddPaisAsync(Pais pais);
        Task UpdatePaisAsync(Pais pais);
        Task DeletePaisAsync(int id);
    }
}
