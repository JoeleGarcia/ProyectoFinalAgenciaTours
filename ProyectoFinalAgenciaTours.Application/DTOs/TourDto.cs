namespace ProyectoFinalAgenciaTours.Application.DTOs
{
    public record CreateTourDto(string Nombre, int PaisId, int DestinoId, DateTime Fecha, TimeSpan Hora, int Horas, decimal TasaImpuesto, decimal Precio, decimal ITBIS, DateTime FechaFinalizacion);
    public record UpdateTourDto(int Id, string Nombre, int PaisId, int DestinoId, DateTime Fecha, TimeSpan Hora, int Horas, decimal TasaImpuesto, decimal Precio, decimal ITBIS, DateTime FechaFinalizacion);
    public record TourDetailDto(int Id, string Nombre, int PaisId, PaisDto Pais, int DestinoId, DestinoDto Destino, DateTime Fecha, TimeSpan Hora, int Horas, decimal TasaImpuesto, decimal Precio, decimal ITBIS, DateTime FechaFinalizacion, string Duracion, string Estado);



    //public record TourDetailDto
    //{
    //    public int Id { get; init; }
    //    public string Nombre { get; init; }



    //    public int PaisId { get; init; }
    //    public PaisDto Pais { get; init; }

    //    public int DestinoId { get; init; }
    //    public DestinoDto Destino { get; init; }



    //    public DateTime Fecha { get; init; }
    //    public TimeSpan Hora { get; init; }


    //    public decimal TasaImpuesto { get; init; }
    //    public decimal Precio { get; init; }


    //    public decimal ITBIS { get; init; }
    //    public DateTime FechaFializacion { get; init; }
    //    public string Duracion { get; init; }
    //    public string Estado { get; init; }



    //    //public string PaisNombre { get; init; }
    //    //public string DestinoNombre { get; init; }

    //    //public int DuracionDiasDestino { get; init; }
    //    //public int DuracionHorasDestino { get; init; }

    //    //public decimal ITBIS { get; init; }

    //    //public decimal Impuestos => Precio * (ITBIS / 100.00m);


    //}
}