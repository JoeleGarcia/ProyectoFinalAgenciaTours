using AutoMapper;
using ProyectoFinalAgenciaTours.Application.DTOs;
using ProyectoFinalAgenciaTours.Application.Exceptions;
using ProyectoFinalAgenciaTours.Application.Interfaces;
using ProyectoFinalAgenciaTours.Domain.Entities;
using ProyectoFinalAgenciaTours.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAgenciaTours.Application.Services
{
    public class TourService : ITourService
    {

        private readonly ITourRepository _tourRepository;
        private readonly IMapper _mapper;

        public TourService(ITourRepository tourRepository, IMapper mapper)
        {
            _tourRepository = tourRepository;
            _mapper = mapper;
        }
        public async Task<CreateTourDto> AddTourAsync(CreateTourDto createTourDto)
        {
            var _createTour = _mapper.Map<Tour>(createTourDto);

            //_createTour.TasaImpuesto = CalculoITBIS(_createTour.Precio, _createTour.ITBIS);
            //DeterminarStatus(_createTour.FechaFinalizacion, _createTour.Hora);
            AutoCalcularValores(_createTour);

            var _tourAgregado = await _tourRepository.AddTourAsync(_createTour);

            var createTour = _mapper.Map<CreateTourDto>(_tourAgregado);

            return createTour;

            //throw new NotImplementedException();
        }

        public void AutoCalcularValores(Tour tour)
        {
            tour.FechaFinalizacion  = tour.Fecha.Add(tour.Hora).AddHours(tour.Horas);
            tour.ITBIS              = CalculoITBIS(tour.Precio, tour.TasaImpuesto);
            tour.Estado             = DeterminarStatus(tour.FechaFinalizacion, tour.Hora);
            tour.Duracion           = Duracion(tour.Fecha, tour.FechaFinalizacion);
            //throw new NotImplementedException();
        }

        public decimal CalculoITBIS(decimal precio, decimal ITBIS)
        {
            return precio * (ITBIS / 100.00m);
        }

        public async Task DeleteTourAsync(int id)
        {
            try
            {
                await _tourRepository.DeleteTourAsync(id);
            }
            catch (InfrastructureException ex)
            {
                throw new InfrastructureException("Timeout al consultar la base de datos", ex);
            }
            catch (NotFoundException ex)
            {
                throw new NotFoundException("Timeout al consultar la base de datos", ex);

            }
            catch (Exception ex)
            {
                throw new NotFoundException("Pais", ex.Message);
            }
        }

        public string DeterminarStatus(DateTime fecha, TimeSpan hora)
        {
            var time = fecha;
            return time > DateTime.Now ? "Vigente" : "No Vigente";
        }

        public string Duracion(DateTime fecha, DateTime fechaFin)
        {
            TimeSpan duracion = fechaFin.Subtract(fecha);

            int dias = duracion.Days;
            int horas = duracion.Hours;

            string resultado = "";
            if (dias > 0) resultado += $"{dias} día{(dias == 1 ? "" : "s")}";
            if (horas > 0)
            {
                if (dias > 0) resultado += " y ";
                resultado += $"{horas} hora{(horas == 1 ? "" : "s")}";
            }

            return string.IsNullOrEmpty(resultado) ? "Menos de una hora" : resultado;
        }

        public async Task<IEnumerable<TourDetailDto>> GetAllTourAsync()
        {
            var tour = await _tourRepository.GetAllTouresAsync();
            var Mapper = _mapper.Map<IEnumerable<TourDetailDto>>(tour);

            return _mapper.Map<IEnumerable<TourDetailDto>>(tour);
        }

        public async Task<TourDetailDto> GetTourByIdAsync(int id)
        {
            var _tour = await _tourRepository.GetTourByIdAsync(id);
            return _mapper.Map<TourDetailDto>(_tour);
        }

        public async Task UpdateTourAsync(int id, UpdateTourDto updateTourDto)
        {

            var _tour = await _tourRepository.GetTourByIdAsync(id);


            if (_tour is null)
            {
                throw new NotFoundException("Tour", id);
            }

            var updatedTour = _mapper.Map<Tour>(updateTourDto);

            AutoCalcularValores(updatedTour);

            await _tourRepository.UpdateTourAsync(updatedTour);
        }
    }
}
