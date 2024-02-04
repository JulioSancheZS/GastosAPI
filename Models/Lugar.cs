using System;
using System.Collections.Generic;

namespace GastosAPI.Models;

public partial class Lugar
{
    public Guid IdLugar { get; set; }

    public string? NombreLugar { get; set; }

    public virtual ICollection<Transaccion> Transaccions { get; set; } = new List<Transaccion>();
}
