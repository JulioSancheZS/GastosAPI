namespace GastosAPI.DTO
{
    public class TasaCambioDTO
    {
        public Guid IdTasaCambio { get; set; }

        public decimal? Tasa { get; set; }

        public string? Dia { get; set; }

        public string? Mes { get; set; }

        public string? Anio { get; set; }
    }
}
