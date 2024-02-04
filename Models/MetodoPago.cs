using System;
using System.Collections.Generic;

namespace GastosAPI.Models;

public partial class MetodoPago
{
    public Guid IdMetodoPago { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<Transaccion> Transaccions { get; set; } = new List<Transaccion>();
}
