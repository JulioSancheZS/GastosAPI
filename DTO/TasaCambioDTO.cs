namespace GastosAPI.DTO
{
    public class TasaCambioDTO
    {
        public Guid IdTasaCambio { get; set; }

        public DateTime? Fecha { get; set; }

        public double? TipoCambio { get; set; }

        public DateTime? FechaRegistro { get; set; }
    }
}
