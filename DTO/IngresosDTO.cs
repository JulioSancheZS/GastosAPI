namespace GastosAPI.DTO
{
    public class IngresosDTO
    {
        public Guid? IdIngreso { get; set; }

        public Guid? IdBalance { get; set; }

        public decimal? Monto { get; set; }

        public DateTime? FechaIngreso { get; set; }
        public Guid? IdUsuario { get; set; }

        public string? Descripcion { get; set; }

    }
}
