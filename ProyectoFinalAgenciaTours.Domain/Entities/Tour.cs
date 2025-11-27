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
        public Destinos Destino { get; set; }


        public DateTime FechaInicio { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public decimal Precio { get; set; }



    }
}
