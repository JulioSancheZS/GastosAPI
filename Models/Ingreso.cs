using System;
using System.Collections.Generic;

namespace GastosAPI.Models;

public partial class Ingreso
{
    public Guid IdIngreso { get; set; }

    public Guid? IdBalance { get; set; }

    public decimal? Monto { get; set; }

    public DateTime? FechaIngreso { get; set; }

    public string? Descripcion { get; set; }

    public virtual Balance? IdBalanceNavigation { get; set; }
}
