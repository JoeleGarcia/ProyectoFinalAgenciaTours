using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAgenciaTours.Domain.Entities
{
    public class Usuario: BaseEntity
    {
        public Guid Id { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public string Role { get; set; }
        public bool Status { get; set; }
        public Usuario()
        {
            Id = Guid.NewGuid();
            FechaCreacion = DateTime.UtcNow;
            FechaModificacion = DateTime.UtcNow;
            Role = "Admin";
            Status = true;
        }

    }
}
