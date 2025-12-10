using ProyectoFinalAgenciaTours.Application.DTOs;
using ProyectoFinalAgenciaTours.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAgenciaTours.Application.Interfaces
{
    public interface ITourService
    {
        Task<IEnumerable<TourDetailDto>> GetAllTourAsync();
        Task<TourDetailDto> GetTourByIdAsync(int id);
        Task<CreateTourDto> AddTourAsync(CreateTourDto createTourDto);
        Task UpdateTourAsync(int id, UpdateTourDto updateTourDto);
        Task DeleteTourAsync(int id);
        void AutoCalcularValores(Tour tour);
        string DeterminarStatus(DateTime fecha, TimeSpan hora);
        string Duracion(DateTime fecha, DateTime fechaFin);
        decimal CalculoITBIS(decimal precio, decimal ITBIS);

    }
}
