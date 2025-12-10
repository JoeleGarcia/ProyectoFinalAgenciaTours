using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAgenciaTours.Domain.Entities
{
    public class Tour: BaseEntity
    {
        public int Id { get; set; }
        public string Nombre { get; set; }


        public int PaisId { get; set; }
        public Pais Pais { get; set; }


        public int DestinoId { get; set; }
        public Destino Destino { get; set; }


        public DateTime Fecha { get; set; }
        public TimeSpan Hora { get; set; }
        public int Horas { get; set; }


        public decimal TasaImpuesto { get; set; }
        public decimal Precio { get; set; }


        public decimal ITBIS { get; set; }
        public DateTime FechaFinalizacion { get; set; }
        public string Duracion { get; set;}
        public string Estado { get; set; }



    }
}
