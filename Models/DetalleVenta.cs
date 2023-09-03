using System;
using System.Collections.Generic;

namespace Tienda.Models;

public partial class DetalleVenta
{
    public int DetalleVentaId { get; set; }

    public int? VentaId { get; set; }

    public int? ProductoId { get; set; }

    public int? Cantidad { get; set; }

    public double? Valor { get; set; }

    public virtual Producto Producto { get; set; } = null!;

    public virtual Venta Venta { get; set; } = null!;
}
