using System;
using System.Collections.Generic;

namespace Tienda.Models;

public partial class Producto
{

    public Producto()
    {

        DetalleVenta = new HashSet<DetalleVenta>();

    }
    public int ProductoId { get; set; }

    public string? Codigo { get; set; }

    public string? NombreProducto { get; set; }

    public double? Precio { get; set; }

    public int? Cantidad { get; set; }

    public virtual ICollection<DetalleVenta> DetalleVenta { get; set; } /*= new List<DetalleVenta>();*/
}
