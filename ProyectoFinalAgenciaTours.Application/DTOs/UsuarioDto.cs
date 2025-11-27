namespace ProyectoFinalAgenciaTours.Application.DTOs
{
    public record UsuarioDto(Guid Id, string? Nombre, string Apellido, string Username, string Email, string Role, bool Status);
    public record LoginUsuarioDto(string Email, string Password);
    public record RegistroUsuarioDto(string Nombre, string Apellido, string Username, string Email, string Password, string ConfirmPassword);

}
