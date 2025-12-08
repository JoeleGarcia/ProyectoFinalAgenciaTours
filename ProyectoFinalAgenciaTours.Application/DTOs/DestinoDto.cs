namespace ProyectoFinalAgenciaTours.Application.DTOs
{
    public record DestinoDto
    {
        public int Id { get; init; }
        public string Nombre { get; init; }
        public int CapacidadMaxima { get; init; }
        public string TipoDestino { get; init; }
        public int PaisId { get; init; }
        public PaisDto Pais { get; init; }
        public bool Status { get; set; }
        public DateTime FechaCreacion { get; init; }

    }
    public record CreateDestinoDto(string Nombre, int CapacidadMaxima, string TipoDestino, int PaisId , bool Status);
    public record UpdateDestinoDto(int Id, string Nombre, int CapacidadMaxima, string TipoDestino, int PaisId, bool Status);
}