namespace ProyectoFinalAgenciaTours.Application.DTOs
{
    public record PaisDto
    {
        public int Id { get; init; }
        public string Nombre { get; init; }
        public string CodigoISO { get; init; }
        public string Moneda { get; init; }
        public string IdiomaOficial { get; init; }
        public bool RequiereVisa { get; init; }
        public bool Status { get; init; }
        public DateTime FechaCreacion { get; init; }
        public DateTime? FechaModificacion { get; init; }
    }

    public record CreatePaisDto(string Nombre, string CodigoISO, string Moneda, string IdiomaOficial, bool RequiereVisa, bool Status);
    public record UpdatePaisDto(int Id, string Nombre, string CodigoISO, string Moneda,  string IdiomaOficial, bool RequiereVisa, bool Status);

}
