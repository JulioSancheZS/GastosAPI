using GastosAPI.Models;

namespace GastosAPI.DTO
{
    public class BalanceDTO
    {
        public Guid IdBalance { get; set; }

        public Guid? IdUsuario { get; set; }

        public decimal? Monto { get; set; }

        public DateTime? FechaActualizacion { get; set; }
    }
}
