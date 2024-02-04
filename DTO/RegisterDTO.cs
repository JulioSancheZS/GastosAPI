namespace GastosAPI.DTO
{
    public class RegisterDTO
    {
        public Guid IdUsuario { get; set; }

        public string? Password { get; set; }

        public string? PaswordHash { get; set; }

        public string? Correo { get; set; }

        public string? PrimerNombre { get; set; }

        public string? SegundoNombre { get; set; }

        public DateTime? FechaRegistro { get; set; }
    }
}
