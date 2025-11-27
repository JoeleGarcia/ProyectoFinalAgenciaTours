using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAgenciaTours.Domain.Entities
{
    public class Destinos: BaseEntity
    {

        public int Id { get; set; }
        public string Nombre { get; set; }

 
        public int CapacidadMaxima { get; set; } // Para futuras reservaciones
        public string TipoDestino { get; set; } // Ejemplo: "Playa", "Montaña", "Histórico"


        public int PaisId { get; set; }
        public Pais Pais { get; set; }
        

        public Destinos()
        {
            FechaCreacion = DateTime.UtcNow;
        }


    }
}
