using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAgenciaTours.Domain.Entities
{
    public class Destino: BaseEntity
    {

        public int Id { get; set; }
        public string Nombre { get; set; }

 
        public int CapacidadMaxima { get; set; }
        public string TipoDestino { get; set; }


        public int PaisId { get; set; }
        public Pais Pais { get; set; }

        public bool Status { get; set; }

        public Destino()
        {
            FechaCreacion = DateTime.UtcNow;
        }

    }
}
