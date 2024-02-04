using System;
using System.Collections.Generic;

namespace GastosAPI.Models;

public partial class Transaccion
{
    public Guid IdTransaccion { get; set; }

    public Guid? IdUsuario { get; set; }

    public Guid? IdCategoria { get; set; }

    public Guid? IdLugar { get; set; }

    public Guid? IdMetodoPago { get; set; }

    public string? Descripcion { get; set; }

    public string? Producto { get; set; }

    public decimal? Monto { get; set; }

    public DateTime? FechaTransaccion { get; set; }

    public virtual Categoria? IdCategoriaNavigation { get; set; }

    public virtual Lugar? IdLugarNavigation { get; set; }

    public virtual MetodoPago? IdMetodoPagoNavigation { get; set; }

    public virtual Usuario? IdUsuarioNavigation { get; set; }
}
