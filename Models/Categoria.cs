using System;
using System.Collections.Generic;

namespace GastosAPI.Models;

public partial class Categoria
{
    public Guid IdCategoria { get; set; }

    public string? NombreCategoria { get; set; }

    public string? Icono { get; set; }

    public string? Color { get; set; }

    public virtual ICollection<Transaccion> Transaccions { get; set; } = new List<Transaccion>();
}
