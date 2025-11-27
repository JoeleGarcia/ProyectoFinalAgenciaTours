namespace ProyectoFinalAgenciaTours.Application.DTOs
{
    public record CreateTourDto(string Nombre, int PaisId, int DestinoId, DateTime FechaInicio, TimeSpan HoraInicio, decimal Precio);
    public record UpdateTourDto(int Id, string Nombre, int PaisId, int DestinoId, DateTime FechaInicio, TimeSpan HoraInicio, decimal Precio);
    public record TourDetailDto(int Id, string Nombre, decimal Precio, DateTime FechaInicio, TimeSpan HoraInicio, string PaisNombre, string DestinoNombre, int DuracionDiasDestino, int DuracionHorasDestino)
    {
        public decimal ITBIS => Precio * 0.18m;
        public TimeSpan DuracionTotal
        {
            get
            {
                TimeSpan duration = TimeSpan.FromDays(DuracionDiasDestino);
                return duration.Add(TimeSpan.FromHours(DuracionHorasDestino));
            }
        }
        public DateTime FechaHoraFin
        {
            get
            {
                var fechaHoraInicio = FechaInicio.Date.Add(HoraInicio);
                return fechaHoraInicio.Add(DuracionTotal);
            }
        }
        public string Estado => FechaHoraFin > DateTime.UtcNow ? "Vigente" : "No Vigente";
    }
}