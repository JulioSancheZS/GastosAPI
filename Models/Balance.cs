using System;
using System.Collections.Generic;

namespace GastosAPI.Models;

public partial class Balance
{
    public Guid IdBalance { get; set; }

    public Guid? IdUsuario { get; set; }

    public decimal? Monto { get; set; }

    public DateTime? FechaActualizacion { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
