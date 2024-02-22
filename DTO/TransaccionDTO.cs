namespace GastosAPI.DTO
{
    public class TransaccionDTO
    {
        public Guid IdTransaccion { get; set; }

        public Guid? IdUsuario { get; set; }

        public Guid? IdCategoria { get; set; }

        public Guid? IdLugar { get; set; }

        public Guid? IdMetodoPago { get; set; }

        public string? Descripcion { get; set; }

        public string? Producto { get; set; }

        public decimal? Monto { get; set; }

        public DateTime? FechaTransaccion { get; set; }

        public string? NombreCategoria { get; set; }
        public string? NombreLugar { get; set; }
        public string? NombreMetodoPago { get; set; }
    }
}
