using System;
using System.Collections.Generic;

namespace GastosAPI.Models;

public partial class TasaCambio
{
    public Guid IdTasaCambio { get; set; }

    public DateTime? Fecha { get; set; }

    public double? TipoCambio { get; set; }

    public DateTime? FechaRegistro { get; set; }
}
