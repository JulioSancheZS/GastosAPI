using System;
using System.Collections.Generic;

namespace GastosAPI.Models;

public partial class Usuario
{
    public Guid IdUsuario { get; set; }

    public string? Password { get; set; }

    public string? PaswordHash { get; set; }

    public string? Correo { get; set; }

    public string? PrimerNombre { get; set; }

    public string? SegundoNombre { get; set; }

    public DateTime? FechaRegistro { get; set; }

    public virtual ICollection<Balance> Balances { get; set; } = new List<Balance>();

    public virtual ICollection<Transaccion> Transaccions { get; set; } = new List<Transaccion>();
}
