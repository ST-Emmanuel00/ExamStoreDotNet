using System;
using System.Collections.Generic;

namespace Tienda.Models;

public partial class Venta
{

    public Venta()
    {
        DetalleVenta = new HashSet<DetalleVenta>();
    }
    public int VentaId { get; set; }

    public string? Factura { get; set; }

    public int? ClienteId { get; set; }

    public double? Total { get; set; }

    public DateTime? FechaVenta { get; set; }

    public virtual Cliente Cliente { get; set; } = null!;

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } /*= new List<DetalleVenta>();*/
}
