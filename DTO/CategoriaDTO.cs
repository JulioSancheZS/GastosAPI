using System.ComponentModel.DataAnnotations;

namespace GastosAPI.DTO
{
    public class CategoriaDTO
    {
        public Guid? IdCategoria { get; set; }

        [Required]
        public string? NombreCategoria { get; set; }
        [Required]
        public string? Icono { get; set; }
        [Required]
        public string? Color { get; set; }
    }
}
