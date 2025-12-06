using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAgenciaTours.Domain.Entities
{
    public class Pais: BaseEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string CodigoISO { get; set; }
        public string Moneda { get; set; } 
        public string IdiomaOficial { get; set; }
        public bool RequiereVisa { get; set; }
        public bool Status { get; set; }

        public ICollection<Destinos> Destinos { get; set; }

        public Pais()
        {
            FechaCreacion = DateTime.UtcNow;
            Status = true;
            IdiomaOficial = "N/A";
        }


    }
}
