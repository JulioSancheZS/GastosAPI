namespace GastosAPI.DTO
{
    public class UsuarioDTO
    {
        public Guid IdUsuario { get; set; }

        public string? Password { get; set; }

        public string? PaswordHash { get; set; } //Salt

        public string? Correo { get; set; }

        public string? NombreCompleto { get; set; }

        public string? PrimerNombre { get; set; }

        public string? SegundoNombre { get; set; }

        public DateTime? FechaRegistro { get; set; }
    }
}
