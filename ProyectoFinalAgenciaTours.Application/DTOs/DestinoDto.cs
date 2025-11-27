namespace ProyectoFinalAgenciaTours.Application.DTOs
{
    public record DestinoDto(int Id, string Nombre, int CapacidadMaxima, string TipoDestino, int PaisId, DateTime FechaCreacion);
    public record CreateDestinoDto(string Nombre, int CapacidadMaxima, string TipoDestino, int PaisId);
    public record UpdateDestinoDto(int Id, string Nombre, int CapacidadMaxima, string TipoDestino, int PaisId);
}