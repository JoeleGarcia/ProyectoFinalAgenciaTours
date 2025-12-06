using ProyectoFinalAgenciaTours.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAgenciaTours.Application.Interfaces
{
    public interface IPaisService
    {
        Task<IEnumerable<PaisDto>> GetAllPaisAsync();
        Task<PaisDto> GetPaisByIdAsync(int id);
        Task<CreatePaisDto> AddPaisAsync(CreatePaisDto createPaisDto);
        Task UpdatePaisAsync(int id, UpdatePaisDto updatePaisDto);
        Task DeletePaisAsync(int id);
    }
}
