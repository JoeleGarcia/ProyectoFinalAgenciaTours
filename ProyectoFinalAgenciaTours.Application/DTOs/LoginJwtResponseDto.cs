
namespace ProyectoFinalAgenciaTours.Application.DTOs
{
    public record LoginJwtResponseDto (
        string UserName,
        string AccessToken,
        int ExpiresIn
    );

}
