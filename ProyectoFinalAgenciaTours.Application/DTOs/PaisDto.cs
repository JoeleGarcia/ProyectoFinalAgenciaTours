namespace ProyectoFinalAgenciaTours.Application.DTOs
{
    public record PaisDto(int Id, string Nombre, string CodigoISO, string Moneda, string ZonaHorariaBase, bool RequiereVisa, DateTime FechaCreacion);
    public record CreatePaisDto(string Nombre, string CodigoISO, string Moneda, string ZonaHorariaBase, bool RequiereVisa);
    public record UpdatePaisDto(int Id, string Nombre, string CodigoISO, string Moneda, string ZonaHorariaBase, bool RequiereVisa);

}
