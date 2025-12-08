using ProyectoFinalAgenciaTours.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAgenciaTours.Application.Interfaces
{
    public interface IDestinoService
    {
        Task<IEnumerable<DestinoDto>> GetAllDestinoAsync();
        Task<DestinoDto> GetDestinoByIdAsync(int id);
        Task<CreateDestinoDto> AddDestinoAsync(CreateDestinoDto createDestinoDto);
        Task UpdateDestinoAsync(int id, UpdateDestinoDto updateDestinoDto);
        Task DeleteDestinoAsync(int id);
    }
}
